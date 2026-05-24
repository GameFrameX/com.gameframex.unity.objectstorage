<p align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />
</p>

<h1 align="center">Game Frame X Object Storage</h1>

<p align="center">
  <a href="https://github.com/GameFrameX/com.gameframex.unity.objectstorage/releases">
    <img src="https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.objectstorage" alt="Version" />
  </a>
  <a href="https://github.com/GameFrameX/com.gameframex.unity.objectstorage/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.objectstorage" alt="License" />
  </a>
  <a href="https://gameframex.doc.alianblank.com">
    <img src="https://img.shields.io/badge/Documentation-online-blue" alt="Documentation" />
  </a>
</p>

<p align="center">
  独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">文档</a> ·
  <a href="#快速开始">快速开始</a> ·
  <a href="https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6">QQ群</a> ·
  语言: <a href="README.md">English</a> | <strong>简体中文</strong> | <a href="README.zh-TW.md">繁體中文</a> | <a href="README.ja.md">日本語</a> | <a href="README.ko.md">한국어</a>
</p>

---

## 项目简介

**Object Storage 组件** - 提供对象存储（云文件存储）操作的基础接口和抽象。本包定义了上传文件和目录到对象存储服务的核心契约，服务商特定的实现以独立包的形式提供。

### 功能特性

- `IObjectStorageManager` 接口，用于存储管理器初始化
- `IObjectStorageUploadManager` 接口，用于文件/目录上传操作
- `BaseObjectStorageUploadManager` 抽象基类，供自定义实现
- `ObjectStorageUploadFactory` 静态工厂类，用于创建上传管理器实例

## 快速开始

### 安装方式

任选其一：

1. 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容
   ```json
   {"com.gameframex.unity.objectstorage": "https://github.com/AlianBlank/com.gameframex.unity.objectstorage.git"}
   ```
2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式添加库，地址为：https://github.com/AlianBlank/com.gameframex.unity.objectstorage.git
3. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

### 使用示例

```csharp
using GameFrameX.ObjectStorage.Runtime;

// 创建并初始化上传管理器实例
IObjectStorageUploadManager uploadManager = ObjectStorageUploadFactory.Create<YourCustomUploadManager>(
    "your_access_key", "your_secret_key", "your_bucket_name");

// 设置上传文件的目标存储路径
uploadManager.SetSavePath("desired/upload/path");

// 上传本地目录
uploadManager.UploadDirectory("local/directory/to/upload");
```

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

## 开源协议

本项目基于 [LICENSE](LICENSE) 文件中定义的条款授权。
