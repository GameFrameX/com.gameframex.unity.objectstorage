<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Object Storage

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Project Overview

**Object Storage Component** - Provides base interfaces and abstractions for object storage (cloud file storage) operations. This package defines the core contracts for uploading and downloading files and directories to and from object storage services through an Option-based API, usable in both runtime and editor builds, with provider-specific implementations available as separate packages.

### Features

- `IObjectStorageManager` interface: unified contracts for file/directory upload and download, available in both runtime and editor
- `ObjectStorageOptions`, `ObjectStorageUploadOptions`, `ObjectStorageDownloadOptions`: Option-style parameter classes
- `BaseObjectStorageManager` abstract base class for custom implementations (object key combining, directory traversal, error handling)
- `ObjectStorageManagerFactory` static factory for creating and initializing manager instances

## Usage

```csharp
using GameFrameX.ObjectStorage.Runtime;
using GameFrameX.ObjectStorage.Tencent.Runtime; // pick the matching provider package (ALiYun / QiNiu / Tencent)

// Create and initialize the manager through the factory
IObjectStorageManager manager = ObjectStorageManagerFactory.Create<TencentCloudObjectStorageManager>(new ObjectStorageOptions
{
    AccessKey = "your-access-key",
    SecretKey = "your-secret-key",
    BucketName = "your-bucket",
    Endpoint = "ap-guangzhou",  // ALiYun: oss-cn-shenzhen.aliyuncs.com (required); Tencent: bucket region; QiNiu: ignored
    SavePath = "builder/MyGame/Android",  // default object key prefix
});

// Upload a single file (object key = SavePath + file name)
manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = "/build/MyGame.apk" });

// Recursively upload a directory, preserving relative paths
manager.UploadDirectory(new ObjectStorageUploadOptions { LocalPath = "Bundles/Android" });

// Download a single object (local directories are created automatically)
manager.DownloadFile(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android/MyGame.apk",
    LocalPath = "downloads/MyGame.apk",
});

// Download all objects under a prefix, preserving relative paths
manager.DownloadDirectory(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android",
    LocalPath = "downloads",
});
```

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.objectstorage": "https://github.com/gameframex/com.gameframex.unity.objectstorage.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.objectstorage.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.
## Platform Support

| Platform | Supported |
|----------|-----------|
| Windows | Yes |
| macOS | Yes |
| Linux | Yes |
| Android | Yes |
| iOS | Yes |

## Documentation & Resources

- [Documentation](https://gameframex.doc.alianblank.com)
- [GitHub Repository](https://github.com/GameFrameX/com.gameframex.unity.objectstorage)

## Community & Support

- QQ Group: Join via [QR Code](https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6)


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |


## Changelog

See [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.objectstorage/releases) for changelog.
## License

See [LICENSE.md](LICENSE.md) for license information.
