using System;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 对象存储初始化参数
    /// </summary>
    [Serializable]
    public sealed class ObjectStorageOptions
    {
        /// <summary>
        /// 云厂商 AccessKey
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string AccessKey { get; set; }

        /// <summary>
        /// 云厂商 SecretKey
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string SecretKey { get; set; }

        /// <summary>
        /// 存储桶名称
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string BucketName { get; set; }

        /// <summary>
        /// 地址。阿里云为 Endpoint（如 oss-cn-shenzhen.aliyuncs.com，必填）；腾讯云为存储桶地域（如 ap-guangzhou）；七牛云预留（当前忽略）。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string Endpoint { get; set; }

        /// <summary>
        /// 桶内默认根路径。上传时作为对象键前缀，可被每次调用的 <see cref="ObjectStorageUploadOptions.SavePath"/> 覆盖。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string SavePath { get; set; }

        /// <summary>
        /// 下载用的外链域名（如 https://bucket.example.com）。七牛云下载时必填，其他实现忽略。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string DownloadDomain { get; set; }
    }
}
