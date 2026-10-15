using System;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 下载参数
    /// </summary>
    [Serializable]
    public sealed class ObjectStorageDownloadOptions
    {
        /// <summary>
        /// 对象存储上的对象键（DownloadFile）或对象键前缀（DownloadDirectory）
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string ObjectKey { get; set; }

        /// <summary>
        /// 本地目标文件路径（DownloadFile）或本地目标目录路径（DownloadDirectory）
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string LocalPath { get; set; }
    }
}
