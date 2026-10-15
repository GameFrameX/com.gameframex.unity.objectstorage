using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using GameFrameX.ObjectStorage.Runtime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GameFrameX.ObjectStorage.Tests
{
    /// <summary>
    /// 记录型对象存储管理器，用于校验基类的对象键拼接、目录遍历与本地目录创建逻辑，无网络请求。
    /// </summary>
    internal sealed class RecordObjectStorageManager : BaseObjectStorageManager
    {
        public readonly List<string> UploadedObjectKeys = new List<string>();
        public readonly List<string> DownloadedObjectKeys = new List<string>();
        public List<string> ObjectKeysToList = new List<string>();

        protected override bool UploadFileInternal(string objectKey, string localFilePath)
        {
            UploadedObjectKeys.Add(objectKey);
            return true;
        }

        protected override bool DownloadFileInternal(string objectKey, string localFilePath)
        {
            DownloadedObjectKeys.Add(objectKey);
            File.WriteAllText(localFilePath, objectKey);
            return true;
        }

        protected override IEnumerable<string> ListObjectKeys(string prefix)
        {
            return ObjectKeysToList;
        }
    }

    internal class ObjectStorageManagerTests
    {
        private RecordObjectStorageManager _manager;
        private string _rootDirectory;

        [SetUp]
        public void Setup()
        {
            _manager = new RecordObjectStorageManager();
            _rootDirectory = Path.Combine(Path.GetTempPath(), "GameFrameXObjectStorageTests", Path.GetRandomFileName());
            Directory.CreateDirectory(_rootDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_rootDirectory))
            {
                Directory.Delete(_rootDirectory, true);
            }
        }

        [Test]
        public void Init_WithoutBucketName_Throws()
        {
            Assert.Throws<ArgumentException>(() => _manager.Init(new ObjectStorageOptions()));
        }

        [Test]
        public void Init_Twice_KeepsFirstOptions()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket", SavePath = "first/path" });
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket", SavePath = "second/path" });
            string localFilePath = CreateLocalFile("a.bundle");

            _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = localFilePath });

            Assert.AreEqual(new[] { "first/path/a.bundle" }, _manager.UploadedObjectKeys.ToArray());
        }

        [Test]
        public void UploadFile_CombinesDefaultSavePathWithFileName()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket", SavePath = "test/aa/zz/" });
            string localFilePath = CreateLocalFile("a.bundle");

            bool isSuccess = _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = localFilePath });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "test/aa/zz/a.bundle" }, _manager.UploadedObjectKeys.ToArray());
        }

        [Test]
        public void UploadFile_PerCallSavePathOverridesDefault()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket", SavePath = "default/path" });
            string localFilePath = CreateLocalFile("a.bundle");

            bool isSuccess = _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = localFilePath, SavePath = "override/path/" });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "override/path/a.bundle" }, _manager.UploadedObjectKeys.ToArray());
        }

        [Test]
        public void UploadFile_WithoutSavePath_UsesFileNameAsObjectKey()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket" });
            string localFilePath = CreateLocalFile("a.bundle");

            bool isSuccess = _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = localFilePath });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "a.bundle" }, _manager.UploadedObjectKeys.ToArray());
        }

        [Test]
        public void UploadFile_LocalFileMissing_ReturnsFalse()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket" });
            LogAssert.Expect(LogType.Error, new Regex("本地文件不存在"));

            bool isSuccess = _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = Path.Combine(_rootDirectory, "missing.bundle") });

            Assert.IsFalse(isSuccess);
            Assert.IsEmpty(_manager.UploadedObjectKeys);
        }

        [Test]
        public void UploadFile_BeforeInit_ReturnsFalse()
        {
            string localFilePath = CreateLocalFile("a.bundle");
            LogAssert.Expect(LogType.Error, new Regex("未初始化"));

            bool isSuccess = _manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = localFilePath });

            Assert.IsFalse(isSuccess);
        }

        [Test]
        public void UploadDirectory_PreservesHierarchyAndSkipsDotDirectories()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket", SavePath = "builder/1.0.0" });
            CreateLocalFile("root.bundle");
            CreateLocalFile("sub/nested.bundle");
            CreateLocalFile(".hidden/skip.bundle");

            bool isSuccess = _manager.UploadDirectory(new ObjectStorageUploadOptions { LocalPath = _rootDirectory });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "builder/1.0.0/root.bundle", "builder/1.0.0/sub/nested.bundle" }, _manager.UploadedObjectKeys.ToArray());
        }

        [Test]
        public void DownloadFile_CreatesMissingLocalDirectories()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket" });
            string localFilePath = Path.Combine(_rootDirectory, "out", "nested", "a.bundle");

            bool isSuccess = _manager.DownloadFile(new ObjectStorageDownloadOptions { ObjectKey = "test/dir/a.bundle", LocalPath = localFilePath });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "test/dir/a.bundle" }, _manager.DownloadedObjectKeys.ToArray());
            Assert.IsTrue(File.Exists(localFilePath));
        }

        [Test]
        public void DownloadDirectory_MapsObjectKeysToRelativeLocalPaths()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket" });
            _manager.ObjectKeysToList = new List<string> { "bundles/1.0.0/a.bundle", "bundles/1.0.0/sub/b.bundle", "bundles/1.0.0/folder/" };

            bool isSuccess = _manager.DownloadDirectory(new ObjectStorageDownloadOptions { ObjectKey = "bundles/1.0.0", LocalPath = _rootDirectory });

            Assert.IsTrue(isSuccess);
            Assert.AreEqual(new[] { "bundles/1.0.0/a.bundle", "bundles/1.0.0/sub/b.bundle" }, _manager.DownloadedObjectKeys.ToArray());
            Assert.IsTrue(File.Exists(Path.Combine(_rootDirectory, "a.bundle")));
            Assert.IsTrue(File.Exists(Path.Combine(_rootDirectory, "sub", "b.bundle")));
        }

        [Test]
        public void DownloadFile_MissingObjectKey_ReturnsFalse()
        {
            _manager.Init(new ObjectStorageOptions { BucketName = "bucket" });
            LogAssert.Expect(LogType.Error, new Regex("下载参数无效"));

            bool isSuccess = _manager.DownloadFile(new ObjectStorageDownloadOptions { ObjectKey = "", LocalPath = Path.Combine(_rootDirectory, "a.bundle") });

            Assert.IsFalse(isSuccess);
        }

        private string CreateLocalFile(string relativePath)
        {
            string localFilePath = Path.Combine(_rootDirectory, relativePath);
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(localFilePath));
            if (!directory.Exists)
            {
                directory.Create();
            }

            File.WriteAllText(localFilePath, relativePath);
            return localFilePath;
        }
    }
}
