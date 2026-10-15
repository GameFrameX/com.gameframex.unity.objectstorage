using System;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 上传参数
    /// </summary>
    [Serializable]
    public sealed class ObjectStorageUploadOptions
    {
        /// <summary>
        /// 本地文件路径（UploadFile）或本地目录路径（UploadDirectory）
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string LocalPath { get; set; }

        /// <summary>
        /// 桶内目标根路径。为空时使用初始化 <see cref="ObjectStorageOptions.SavePath"/> 配置的默认值。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string SavePath { get; set; }
    }
}
