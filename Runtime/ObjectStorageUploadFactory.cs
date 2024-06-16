using System;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 对象存储上传工厂
    /// </summary>
    public static class ObjectStorageUploadFactory
    {
        /// <summary>
        /// 创建对象存储上传管理器。且初始化
        /// </summary>
        /// <param name="accessKey">key</param>
        /// <param name="secretKey">秘钥</param>
        /// <param name="bucketName">存储池名称</param>
        /// <param name="endpoint">地址,有些对象存储不需要。</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回已经调用了初始化函数的对象存储上传管理器</returns>
        public static IObjectStorageUploadManager Create<T>(string accessKey, string secretKey, string bucketName, string endpoint = null) where T : IObjectStorageUploadManager
        {
            var manager = Activator.CreateInstance<T>();
            manager.Init(accessKey, secretKey, bucketName, endpoint);
            return manager;
        }
    }
}