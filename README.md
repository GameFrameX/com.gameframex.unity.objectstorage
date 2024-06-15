## HOMEPAGE

GameFrameX 的 Object Storage 组件

**Object Storage 组件 (Object Storage Component)** - 提供Object Storage组件相关的接口。

# 使用文档(文档编写于GPT4)

# 功能说明文档

## 接口和类概览

本文档涵盖了以下接口和类的功能说明：

1. `IObjectStorageManager` - 定义了对象存储管理器的初始化方法。
2. `IObjectStorageUploadManager` - 扩展了 `IObjectStorageManager`，添加了上传相关的功能。
3. `BaseObjectStorageUploadManager` - 抽象基类，提供了上传管理器的基本框架。
4. `ObjectStorageUploadFactory` - 静态工厂类，用于创建和初始化上传管理器实例。

## 接口和类详细说明

### IObjectStorageManager

#### 功能描述

`IObjectStorageManager` 接口定义了对象存储管理器的初始化方法，为后续的对象存储操作提供必要的环境配置。

#### 方法说明

- `Init(string accessKey, string secretKey, string bucketName)`
    - 初始化对象存储管理器，设置访问密钥、秘密密钥和存储桶名称。

### IObjectStorageUploadManager

#### 功能描述

`IObjectStorageUploadManager` 接口继承自 `IObjectStorageManager`，并在此基础上增加了上传文件和目录的功能。

#### 方法说明

- `SetSavePath(string savePath)`
    - 设置上传文件的目标存储路径。
- `UploadDirectory(string localDirectory)`
    - 将指定本地目录下的所有文件上传到对象存储服务。

### BaseObjectStorageUploadManager

#### 功能描述

`BaseObjectStorageUploadManager` 是一个抽象类，它实现了 `IObjectStorageUploadManager` 接口的部分功能，并提供了一个抽象方法供子类实现具体的上传逻辑。

#### 属性说明

- `BucketSavePath`
    - 存储上传文件的目标路径。
- `UploadRootPath`
    - 存储待上传的本地目录路径。

#### 方法说明

- `Init(string accessKey, string secretKey, string bucketName)`
    - 抽象方法，由子类实现具体的初始化逻辑。
- `SetSavePath(string savePath)`
    - 设置目标存储路径，确保路径末尾没有多余的目录分隔符。
- `UploadDirectory(string localDirectory)`
    - 设置待上传的本地目录，并调用内部方法执行上传。
- `UploadDirectoryInternal(string localDirectory)`
    - 抽象方法，由子类实现具体的上传逻辑。

### ObjectStorageUploadFactory

#### 功能描述

`ObjectStorageUploadFactory` 是一个静态工厂类，用于创建和初始化上传管理器实例。

#### 方法说明

- `Create<T>(string accessKey, string secretKey, string bucketName)`
    - 创建指定类型的上传管理器实例，并自动调用初始化方法。

## 使用指南

### 引入命名空间

在使用上述接口和类之前，请确保已引入正确的命名空间：

```csharp
using GameFrameX.ObjectStorage.Runtime;
```

### 初始化和使用上传管理器

1. 使用工厂类创建并初始化上传管理器实例：
   ```csharp
   IObjectStorageUploadManager uploadManager = ObjectStorageUploadFactory.Create<YourCustomUploadManager>("your_access_key", "your_secret_key", "your_bucket_name");
   ```
2. 设置上传文件的目标存储路径：
   ```csharp
   uploadManager.SetSavePath("desired/upload/path");
   ```
3. 上传本地目录：
   ```csharp
   uploadManager.UploadDirectory("local/directory/to/upload");
   ```

请根据实际的业务需求和对象存储服务的特性，实现 `BaseObjectStorageUploadManager` 的抽象方法，以提供具体的上传逻辑。

## 注意事项

# 使用方式(任选其一)

1. 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容
   ```json
      {"com.alianblank.gameframex.unity.objectstorage": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.objectstorage.git"}
    ```
2. 在Unity 的`Packages Manager` 中使用`Git URL` 的方式添加库,地址为：https://github.com/AlianBlank/com.alianblank.gameframex.unity.objectstorage.git

3. 直接下载仓库放置到Unity 项目的`Packages` 目录下。会自动加载识别