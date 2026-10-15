<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Object Storage

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

**Object Storage 组件** - 提供对象存储（云文件存储）操作的基础接口和抽象。本包以 Option 风格参数定义了上传、下载文件和目录到对象存储服务的核心契约，运行时与编辑器均可使用，服务商特定的实现以独立包的形式提供。

### 功能特性

- `IObjectStorageManager` 接口：统一的文件/目录上传与下载契约，运行时与编辑器均可用
- `ObjectStorageOptions`、`ObjectStorageUploadOptions`、`ObjectStorageDownloadOptions`：Option 风格的参数类
- `BaseObjectStorageManager` 抽象基类，供自定义实现（负责对象键拼接、目录遍历、异常处理）
- `ObjectStorageManagerFactory` 静态工厂类，用于创建并初始化管理器实例

## 使用示例

```csharp
using GameFrameX.ObjectStorage.Runtime;
using GameFrameX.ObjectStorage.Tencent.Runtime; // 按需选择云厂商实现包（阿里云 / 七牛云 / 腾讯云）

// 通过工厂创建并初始化管理器
IObjectStorageManager manager = ObjectStorageManagerFactory.Create<TencentCloudObjectStorageManager>(new ObjectStorageOptions
{
    AccessKey = "your-access-key",
    SecretKey = "your-secret-key",
    BucketName = "your-bucket",
    Endpoint = "ap-guangzhou",  // 阿里云：oss-cn-shenzhen.aliyuncs.com（必填）；腾讯云：存储桶地域；七牛云：忽略
    SavePath = "builder/MyGame/Android",  // 默认对象键前缀
});

// 上传单个文件（对象键 = SavePath + 文件名）
manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = "/build/MyGame.apk" });

// 递归上传整个目录，保留相对路径
manager.UploadDirectory(new ObjectStorageUploadOptions { LocalPath = "Bundles/Android" });

// 下载单个对象（自动创建本地目录）
manager.DownloadFile(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android/MyGame.apk",
    LocalPath = "downloads/MyGame.apk",
});

// 下载前缀下的全部对象，保留相对路径
manager.DownloadDirectory(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android",
    LocalPath = "downloads",
});
```

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.objectstorage": "1.1.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.objectstorage": "https://github.com/gameframex/com.gameframex.unity.objectstorage.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.objectstorage.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。
## 平台支持

| 平台 | 支持 |
|------|------|
| Windows | 是 |
| macOS | 是 |
| Linux | 是 |
| Android | 是 |
| iOS | 是 |

## 文档与资源

- [文档](https://gameframex.doc.alianblank.com)
- [GitHub 仓库](https://github.com/GameFrameX/com.gameframex.unity.objectstorage)

## 社区与支持

- QQ群：通过 [二维码](https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6) 加入


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |


## 更新日志

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.objectstorage/releases) 了解更新日志。
## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
