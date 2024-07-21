using System.IO;

namespace GameFrameX.ObjectStorage.Runtime
{
    public abstract class BaseObjectStorageUploadManager : IObjectStorageUploadManager
    {
        protected string BucketSavePath;

        protected string UploadRootPath = string.Empty;
        public abstract void Init(string accessKey, string secretKey, string bucketName, string endpoint = null);

        public void SetSavePath(string savePath)
        {
            if (savePath.EndsWith(Path.DirectorySeparatorChar))
            {
                savePath = savePath.Substring(0, savePath.Length - 1);
            }

            BucketSavePath = savePath;
        }

        public void UploadDirectory(string localDirectory)
        {
            UploadRootPath = localDirectory;
            UploadDirectoryInternal(localDirectory);
        }

        public bool UploadFile(string localFilePathAndName)
        {
            return UploadFileInternal(localFilePathAndName);
        }

        protected abstract void UploadDirectoryInternal(string localDirectory);
        protected abstract bool UploadFileInternal(string localFilePathAndName);
    }
}