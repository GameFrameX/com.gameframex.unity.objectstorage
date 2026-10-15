<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Object Storage

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 項目簡介

**Object Storage 組件** - 提供物件儲存（雲端檔案儲存）操作的基礎介面和抽象。本套件以 Option 風格參數定義了上傳、下載檔案和目錄到物件儲存服務的核心合約，執行時與編輯器均可使用，服務商特定的實作以獨立套件的形式提供。

### 功能特性

- `IObjectStorageManager` 介面：統一的檔案/目錄上傳與下載合約，執行時與編輯器均可用
- `ObjectStorageOptions`、`ObjectStorageUploadOptions`、`ObjectStorageDownloadOptions`：Option 風格的參數類別
- `BaseObjectStorageManager` 抽象基底類別，供自訂實作（負責物件鍵拼接、目錄遍歷、例外處理）
- `ObjectStorageManagerFactory` 靜態工廠類別，用於建立並初始化管理器實例

## 使用範例

```csharp
using GameFrameX.ObjectStorage.Runtime;
using GameFrameX.ObjectStorage.Tencent.Runtime; // 按需選擇雲端廠商實作套件（阿里雲 / 七牛雲 / 騰訊雲）

// 透過工廠建立並初始化管理器
IObjectStorageManager manager = ObjectStorageManagerFactory.Create<TencentCloudObjectStorageManager>(new ObjectStorageOptions
{
    AccessKey = "your-access-key",
    SecretKey = "your-secret-key",
    BucketName = "your-bucket",
    Endpoint = "ap-guangzhou",  // 阿里雲：oss-cn-shenzhen.aliyuncs.com（必填）；騰訊雲：儲存桶地域；七牛雲：忽略
    SavePath = "builder/MyGame/Android",  // 預設物件鍵前綴
});

// 上傳單一檔案（物件鍵 = SavePath + 檔名）
manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = "/build/MyGame.apk" });

// 遞迴上傳整個目錄，保留相對路徑
manager.UploadDirectory(new ObjectStorageUploadOptions { LocalPath = "Bundles/Android" });

// 下載單一物件（自動建立本地目錄）
manager.DownloadFile(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android/MyGame.apk",
    LocalPath = "downloads/MyGame.apk",
});

// 下載前綴下的全部物件，保留相對路徑
manager.DownloadDirectory(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android",
    LocalPath = "downloads",
});
```

## 快速開始

### 安裝

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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

   `scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

2. 直接在 `manifest.json` 的 `dependencies` 節點下添加以下內容：
   ```json
   {
      "com.gameframex.unity.objectstorage": "https://github.com/gameframex/com.gameframex.unity.objectstorage.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加庫，地址為：`https://github.com/gameframex/com.gameframex.unity.objectstorage.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。
## 平台支援

| 平台 | 支援 |
|------|------|
| Windows | 是 |
| macOS | 是 |
| Linux | 是 |
| Android | 是 |
| iOS | 是 |

## 文檔與資源

- [文檔](https://gameframex.doc.alianblank.com)
- [GitHub 倉庫](https://github.com/GameFrameX/com.gameframex.unity.objectstorage)

## 社區與支援

- QQ群：透過 [二維碼](https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6) 加入


## 依賴

| 套件 | 說明 |
|------|------|
| (无) | - |


## 更新日誌

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.objectstorage/releases) 了解更新日誌。
## 開源協議

本專案基於 [LICENSE](LICENSE) 文件中定義的條款授權。
