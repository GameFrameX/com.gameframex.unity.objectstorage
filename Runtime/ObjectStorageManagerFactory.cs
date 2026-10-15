using System;

namespace GameFrameX.ObjectStorage.Runtime
{
    /// <summary>
    /// 对象存储管理器工厂
    /// </summary>
    public static class ObjectStorageManagerFactory
    {
        /// <summary>
        /// 创建对象存储管理器并调用初始化
        /// </summary>
        /// <param name="options">初始化参数</param>
        /// <typeparam name="T">对象存储管理器类型，需有无参构造</typeparam>
        /// <returns>返回已经调用了初始化函数的对象存储管理器</returns>
        [UnityEngine.Scripting.Preserve]
        public static T Create<T>(ObjectStorageOptions options) where T : class, IObjectStorageManager, new()
        {
            T manager = new T();
            manager.Init(options);
            return manager;
        }
    }
}
