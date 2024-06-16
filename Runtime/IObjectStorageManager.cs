namespace GameFrameX.ObjectStorage.Runtime
{
    public interface IObjectStorageManager
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="accessKey">key</param>
        /// <param name="secretKey">秘钥</param>
        /// <param name="bucketName">存储池名称</param>
        /// <param name="endpoint">地址</param>
        void Init(string accessKey, string secretKey, string bucketName, string endpoint = null);
    }
}