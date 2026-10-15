using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 对象存储管理器基类。统一处理对象键拼接、目录遍历、本地目录创建与异常捕获，云厂商实现只需关注单文件上传、单文件下载与对象列举。
    /// </summary>
    public abstract class BaseObjectStorageManager : IObjectStorageManager
    {
        /// <summary>
        /// 初始化参数。未初始化时为 null。
        /// </summary>
        protected ObjectStorageOptions Options { get; private set; }

        /// <summary>
        /// 是否已完成初始化
        /// </summary>
        protected bool IsInitialized
        {
            get { return Options != null; }
        }

        /// <summary>
        /// 使用云厂商凭证初始化管理器。重复调用会被忽略。
        /// </summary>
        /// <param name="options">初始化参数</param>
        [UnityEngine.Scripting.Preserve]
        public virtual void Init(ObjectStorageOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (string.IsNullOrEmpty(options.BucketName))
            {
                throw new ArgumentException("BucketName 不能为空", "options");
            }

            if (IsInitialized)
            {
                return;
            }

            Options = options;
        }

        /// <summary>
        /// 上传单个文件。对象键 = SavePath + 文件名。
        /// </summary>
        /// <param name="options">上传参数</param>
        /// <returns>上传成功返回 true</returns>
        [UnityEngine.Scripting.Preserve]
        public bool UploadFile(ObjectStorageUploadOptions options)
        {
            if (!ValidateUploadOptions(options))
            {
                return false;
            }

            FileInfo fileInfo = new FileInfo(options.LocalPath);
            if (!fileInfo.Exists)
            {
                Debug.LogError("上传文件失败，本地文件不存在：" + options.LocalPath);
                return false;
            }

            string objectKey = CombineObjectKey(GetEffectiveSavePath(options), fileInfo.Name);
            try
            {
                return UploadFileInternal(objectKey, fileInfo.FullName);
            }
            catch (Exception exception)
            {
                Debug.LogError("上传文件异常，本地文件路径：" + fileInfo.FullName + " 对象键：" + objectKey + "\n" + exception);
                return false;
            }
        }

        /// <summary>
        /// 递归上传整个本地目录，保留相对目录层级。对象键 = SavePath + 相对路径。
        /// </summary>
        /// <param name="options">上传参数</param>
        /// <returns>目录内全部文件上传成功返回 true</returns>
        [UnityEngine.Scripting.Preserve]
        public bool UploadDirectory(ObjectStorageUploadOptions options)
        {
            if (!ValidateUploadOptions(options))
            {
                return false;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(options.LocalPath);
            if (!directoryInfo.Exists)
            {
                Debug.LogError("上传目录失败，本地目录不存在：" + options.LocalPath);
                return false;
            }

            string savePath = GetEffectiveSavePath(options);
            bool isSuccess = true;
            UploadDirectoryFiles(directoryInfo, directoryInfo, savePath, ref isSuccess);
            return isSuccess;
        }

        /// <summary>
        /// 下载单个对象到本地文件。目标目录不存在时会自动创建。
        /// </summary>
        /// <param name="options">下载参数</param>
        /// <returns>下载成功返回 true</returns>
        [UnityEngine.Scripting.Preserve]
        public bool DownloadFile(ObjectStorageDownloadOptions options)
        {
            if (!ValidateDownloadOptions(options))
            {
                return false;
            }

            string localFilePath = Path.GetFullPath(options.LocalPath);
            CreateLocalDirectory(Path.GetDirectoryName(localFilePath));
            string objectKey = NormalizeObjectKey(options.ObjectKey);
            try
            {
                return DownloadFileInternal(objectKey, localFilePath);
            }
            catch (Exception exception)
            {
                Debug.LogError("下载文件异常，对象键：" + objectKey + " 本地文件路径：" + localFilePath + "\n" + exception);
                return false;
            }
        }

        /// <summary>
        /// 下载指定前缀（目录）下的全部对象到本地目录，保留相对目录层级。目标目录不存在时会自动创建。
        /// </summary>
        /// <param name="options">下载参数</param>
        /// <returns>全部对象下载成功返回 true</returns>
        [UnityEngine.Scripting.Preserve]
        public bool DownloadDirectory(ObjectStorageDownloadOptions options)
        {
            if (!ValidateDownloadOptions(options))
            {
                return false;
            }

            string prefix = options.ObjectKey.TrimEnd('/');
            string localRootPath = Path.GetFullPath(options.LocalPath);
            CreateLocalDirectory(localRootPath);

            List<string> objectKeys;
            try
            {
                objectKeys = new List<string>(ListObjectKeys(prefix));
            }
            catch (Exception exception)
            {
                Debug.LogError("列举对象异常，对象键前缀：" + prefix + "\n" + exception);
                return false;
            }

            bool isSuccess = true;
            foreach (string objectKey in objectKeys)
            {
                if (string.IsNullOrEmpty(objectKey) || objectKey.EndsWith("/"))
                {
                    continue;
                }

                string relativePath = objectKey.Substring(prefix.Length).TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                string localFilePath = Path.Combine(localRootPath, relativePath);
                CreateLocalDirectory(Path.GetDirectoryName(localFilePath));
                try
                {
                    if (!DownloadFileInternal(objectKey, localFilePath))
                    {
                        isSuccess = false;
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError("下载文件异常，对象键：" + objectKey + " 本地文件路径：" + localFilePath + "\n" + exception);
                    isSuccess = false;
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// 上传单个文件的云厂商实现
        /// </summary>
        /// <param name="objectKey">对象键</param>
        /// <param name="localFilePath">本地文件路径</param>
        /// <returns>成功返回 true</returns>
        protected abstract bool UploadFileInternal(string objectKey, string localFilePath);

        /// <summary>
        /// 下载单个对象的云厂商实现
        /// </summary>
        /// <param name="objectKey">对象键</param>
        /// <param name="localFilePath">本地文件路径</param>
        /// <returns>成功返回 true</returns>
        protected abstract bool DownloadFileInternal(string objectKey, string localFilePath);

        /// <summary>
        /// 列举指定前缀下的全部对象键（分页由实现内部处理），用于目录下载
        /// </summary>
        /// <param name="prefix">对象键前缀，可为空串（列举整个桶）</param>
        /// <returns>对象键列表</returns>
        protected abstract IEnumerable<string> ListObjectKeys(string prefix);

        private void UploadDirectoryFiles(DirectoryInfo rootDirectory, DirectoryInfo currentDirectory, string savePath, ref bool isSuccess)
        {
            foreach (FileInfo fileInfo in currentDirectory.GetFiles())
            {
                string relativePath = fileInfo.FullName.Substring(rootDirectory.FullName.Length).Replace('\\', '/').TrimStart('/');
                string objectKey = CombineObjectKey(savePath, relativePath);
                try
                {
                    if (!UploadFileInternal(objectKey, fileInfo.FullName))
                    {
                        isSuccess = false;
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError("上传文件异常，本地文件路径：" + fileInfo.FullName + " 对象键：" + objectKey + "\n" + exception);
                    isSuccess = false;
                }
            }

            foreach (DirectoryInfo directoryInfo in currentDirectory.GetDirectories())
            {
                if (directoryInfo.Name.StartsWith("."))
                {
                    continue;
                }

                UploadDirectoryFiles(rootDirectory, directoryInfo, savePath, ref isSuccess);
            }
        }

        private bool ValidateUploadOptions(ObjectStorageUploadOptions options)
        {
            if (!IsInitialized)
            {
                Debug.LogError("对象存储管理器未初始化，请先调用 Init");
                return false;
            }

            if (options == null || string.IsNullOrEmpty(options.LocalPath))
            {
                Debug.LogError("上传参数无效，LocalPath 不能为空");
                return false;
            }

            return true;
        }

        private bool ValidateDownloadOptions(ObjectStorageDownloadOptions options)
        {
            if (!IsInitialized)
            {
                Debug.LogError("对象存储管理器未初始化，请先调用 Init");
                return false;
            }

            if (options == null || string.IsNullOrEmpty(options.ObjectKey) || string.IsNullOrEmpty(options.LocalPath))
            {
                Debug.LogError("下载参数无效，ObjectKey 与 LocalPath 不能为空");
                return false;
            }

            return true;
        }

        private string GetEffectiveSavePath(ObjectStorageUploadOptions options)
        {
            return string.IsNullOrEmpty(options.SavePath) ? Options.SavePath : options.SavePath;
        }

        /// <summary>
        /// 拼接对象键：savePath + 相对路径，分隔符统一为 /。savePath 为空时直接返回相对路径。
        /// </summary>
        /// <param name="savePath">桶内根路径</param>
        /// <param name="relativePath">相对路径</param>
        /// <returns>对象键</returns>
        protected static string CombineObjectKey(string savePath, string relativePath)
        {
            relativePath = relativePath.Replace('\\', '/').TrimStart('/');
            if (string.IsNullOrEmpty(savePath))
            {
                return relativePath;
            }

            return savePath.Replace('\\', '/').TrimEnd('/') + "/" + relativePath;
        }

        private static string NormalizeObjectKey(string objectKey)
        {
            return objectKey.Replace('\\', '/').TrimStart('/');
        }

        private static void CreateLocalDirectory(string directory)
        {
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
