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
  인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">문서</a> ·
  <a href="#빠른-시작">빠른 시작</a> ·
  <a href="https://qm.qq.com/cgi-bin/qm/qr?k=ikT9gA5m2sKwOyNOfYmQvSAPK_c3GmD6">QQ 그룹</a> ·
  언어: <a href="README.md">English</a> | <a href="README.zh-CN.md">简体中文</a> | <a href="README.zh-TW.md">繁體中文</a> | <a href="README.ja.md">日本語</a> | <strong>한국어</strong>
</p>

---

## 프로젝트 개요

**Object Storage 컴포넌트** - 오브젝트 스토리지(클라우드 파일 스토리지) 작업을 위한 기본 인터페이스와 추상을 제공합니다. 이 패키지는 파일 및 디렉토리를 오브젝트 스토리지 서비스에 업로드하기 위한 핵심 계약을 정의하며, 제공자별 구현은 별도의 패키지로 제공됩니다.

### 기능

- `IObjectStorageManager` 인터페이스: 스토리지 매니저 초기화용
- `IObjectStorageUploadManager` 인터페이스: 파일/디렉토리 업로드 작업용
- `BaseObjectStorageUploadManager` 추상 기본 클래스: 사용자 정의 구현용
- `ObjectStorageUploadFactory` 정적 팩토리 클래스: 업로드 매니저 인스턴스 생성용

## 빠른 시작

### 설치

다음 방법 중 하나를 선택하세요:

1. 프로젝트의 `manifest.json` 파일의 `dependencies` 섹션에 다음 내용을 추가:
   ```json
   {"com.gameframex.unity.objectstorage": "https://github.com/AlianBlank/com.gameframex.unity.objectstorage.git"}
   ```
2. Unity의 `Package Manager`에서 `Git URL`을 사용하여 추가: https://github.com/AlianBlank/com.gameframex.unity.objectstorage.git
3. 리포지토리를 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치 (자동으로 로드됩니다).

### 사용 예시

```csharp
using GameFrameX.ObjectStorage.Runtime;

// 업로드 매니저 인스턴스 생성 및 초기화
IObjectStorageUploadManager uploadManager = ObjectStorageUploadFactory.Create<YourCustomUploadManager>(
    "your_access_key", "your_secret_key", "your_bucket_name");

// 업로드 대상 스토리지 경로 설정
uploadManager.SetSavePath("desired/upload/path");

// 로컬 디렉토리 업로드
uploadManager.UploadDirectory("local/directory/to/upload");
```

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

## 라이선스

이 프로젝트는 [LICENSE](LICENSE) 파일에 정의된 조건에 따라 라이선스가 부여됩니다.
