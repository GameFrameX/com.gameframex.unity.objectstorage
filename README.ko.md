<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Object Storage

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.objectstorage)](https://github.com/GameFrameX/com.gameframex.unity.objectstorage/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 개요

**Object Storage 컴포넌트** - 오브젝트 스토리지(클라우드 파일 스토리지) 작업을 위한 기본 인터페이스와 추상을 제공합니다. 이 패키지는 Option 스타일 파라미터로 파일 및 디렉토리 업로드/다운로드 핵심 계약을 정의합니다. 런타임과 에디터 모두에서 사용할 수 있으며, 제공자별 구현은 별도의 패키지로 제공됩니다.

### 기능

- `IObjectStorageManager` 인터페이스: 파일/디렉토리 업로드 및 다운로드 통합 계약, 런타임과 에디터 모두 사용 가능
- `ObjectStorageOptions`, `ObjectStorageUploadOptions`, `ObjectStorageDownloadOptions`: Option 스타일 파라미터 클래스
- `BaseObjectStorageManager` 추상 기본 클래스: 커스텀 구현용(객체 키 결합, 디렉토리 순회, 예외 처리 담당)
- `ObjectStorageManagerFactory` 정적 팩토리 클래스: 매니저 인스턴스 생성 및 초기화

## 사용 예시

```csharp
using GameFrameX.ObjectStorage.Runtime;
using GameFrameX.ObjectStorage.Tencent.Runtime; // 필요한 제공자 패키지 선택 (ALiYun / QiNiu / Tencent)

// 팩토리로 매니저 생성 및 초기화
IObjectStorageManager manager = ObjectStorageManagerFactory.Create<TencentCloudObjectStorageManager>(new ObjectStorageOptions
{
    AccessKey = "your-access-key",
    SecretKey = "your-secret-key",
    BucketName = "your-bucket",
    Endpoint = "ap-guangzhou",  // ALiYun: oss-cn-shenzhen.aliyuncs.com(필수); Tencent: 버킷 리전; QiNiu: 무시
    SavePath = "builder/MyGame/Android",  // 기본 객체 키 접두사
});

// 단일 파일 업로드 (객체 키 = SavePath + 파일명)
manager.UploadFile(new ObjectStorageUploadOptions { LocalPath = "/build/MyGame.apk" });

// 디렉토리 재귀 업로드, 상대 경로 유지
manager.UploadDirectory(new ObjectStorageUploadOptions { LocalPath = "Bundles/Android" });

// 단일 객체 다운로드 (로컬 디렉토리 자동 생성)
manager.DownloadFile(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android/MyGame.apk",
    LocalPath = "downloads/MyGame.apk",
});

// 접두사 아래 모든 객체 다운로드, 상대 경로 유지
manager.DownloadDirectory(new ObjectStorageDownloadOptions
{
    ObjectKey = "builder/MyGame/Android",
    LocalPath = "downloads",
});
```

## 빠른 시작

### 설치

다음 방법 중 하나를 선택하세요:

1. Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:
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

   `scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

2. `manifest.json`의 `dependencies`에 직접 추가:
   ```json
   {
      "com.gameframex.unity.objectstorage": "https://github.com/gameframex/com.gameframex.unity.objectstorage.git"
   }
   ```
3. Unity의 **Package Manager**에서 **Git URL**을 사용하여 추가: `https://github.com/gameframex/com.gameframex.unity.objectstorage.git`
4. 리포지토리를 Unity 프로젝트의 `Packages` 디렉토리에 클론하세요. 자동으로 로드됩니다.
## 플랫폼 지원

| 플랫폼 | 지원 |
|--------|------|
| Windows | 예 |
| macOS | 예 |
| Linux | 예 |
| Android | 예 |
| iOS | 예 |

## 문서 및 자료

- [문서](https://gameframex.doc.alianblank.com)
- [GitHub 리포지토리](https://github.com/GameFrameX/com.gameframex.unity.objectstorage)

## 커뮤니티 및 지원

- QQ 그룹: [QR 코드](https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6)에서 가입


## 의존성

| 패키지 | 설명 |
|--------|------|
| (无) | - |


## 변경 로그

[Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.objectstorage/releases)에서 변경 로그를 확인하세요.
## 라이선스

이 프로젝트는 [LICENSE](LICENSE) 파일에 정의된 조건에 따라 라이선스가 부여됩니다.
