namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 对象存储管理器抽象。运行时与编辑器环境下均可使用，支持文件与目录的上传和下载。
    /// </summary>
    public interface IObjectStorageManager
    {
        /// <summary>
        /// 使用云厂商凭证初始化管理器。重复调用会被忽略。
        /// </summary>
        /// <param name="options">初始化参数</param>
        void Init(ObjectStorageOptions options);

        /// <summary>
        /// 上传单个文件。对象键 = SavePath + 文件名。
        /// </summary>
        /// <param name="options">上传参数</param>
        /// <returns>上传成功返回 true</returns>
        bool UploadFile(ObjectStorageUploadOptions options);

        /// <summary>
        /// 递归上传整个本地目录，保留相对目录层级。对象键 = SavePath + 相对路径。
        /// </summary>
        /// <param name="options">上传参数</param>
        /// <returns>目录内全部文件上传成功返回 true</returns>
        bool UploadDirectory(ObjectStorageUploadOptions options);

        /// <summary>
        /// 下载单个对象到本地文件。
        /// </summary>
        /// <param name="options">下载参数</param>
        /// <returns>下载成功返回 true</returns>
        bool DownloadFile(ObjectStorageDownloadOptions options);

        /// <summary>
        /// 下载指定前缀（目录）下的全部对象到本地目录，保留相对目录层级。
        /// </summary>
        /// <param name="options">下载参数</param>
        /// <returns>全部对象下载成功返回 true</returns>
        bool DownloadDirectory(ObjectStorageDownloadOptions options);
    }
}
