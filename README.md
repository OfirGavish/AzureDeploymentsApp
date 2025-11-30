# Azure Deployments App - MAUI iOS Test App

A .NET MAUI application created for testing Intune App Wrapping functionality.

## Purpose

This app demonstrates:
- Building iOS apps from Windows using .NET MAUI
- Creating test apps for Intune wrapper testing
- GitHub Actions CI/CD for iOS builds

## Features

- **Azure Deployments Refresh Button**: Main feature showing "Click here to refresh Azure deployments page"
- **Counter Test**: Simple counter to test app functionality
- **Intune MAM Ready**: Designed to be wrapped with Microsoft Intune App Wrapper

## Building the App

### Prerequisites

- .NET 8 SDK
- MAUI workload: `dotnet workload install maui`

### Local Build (Windows)

```powershell
# Restore dependencies
dotnet restore

# Build for Android (works on Windows)
dotnet build -f net8.0-android -c Release

# Build for iOS (requires Mac build host or GitHub Actions)
dotnet build -f net8.0-ios -c Release
```

### GitHub Actions Build

This repository includes a GitHub Actions workflow that builds the iOS app on macOS runners.

1. Push code to GitHub
2. GitHub Actions automatically builds on macOS
3. Download build artifacts from Actions tab

### Creating IPA for Intune Wrapping

To create a signed .ipa file that can be wrapped:

1. **Get Apple Developer Certificates**:
   - Distribution certificate (.p12)
   - Provisioning profile (.mobileprovision)

2. **Add Secrets to GitHub**:
   - `IOS_CERTIFICATE_BASE64`: Base64-encoded certificate
   - `IOS_CERTIFICATE_PASSWORD`: Certificate password
   - `IOS_PROVISIONING_PROFILE_BASE64`: Base64-encoded provisioning profile

3. **Uncomment signing steps in `.github/workflows/build-ios.yml`**

4. **Push to GitHub** - Actions will build signed IPA

5. **Download IPA artifact** from GitHub Actions

6. **Wrap with Intune**:
   ```powershell
   cd C:\Users\gavishofir\source\Intune-iOS-App
   .\scripts\wrap-app.ps1 `
       -InputIPA "path\to\downloaded.ipa" `
       -OutputIPA "path\to\wrapped.ipa" `
       -ProvisioningProfile "path\to\profile.mobileprovision" `
       -SigningIdentity "iPhone Distribution: Your Company"
   ```

## Project Structure

```
AzureDeploymentsApp/
├── .github/
│   └── workflows/
│       └── build-ios.yml          # GitHub Actions workflow
├── Platforms/
│   ├── Android/                   # Android-specific code
│   ├── iOS/                       # iOS-specific code
│   ├── MacCatalyst/              # macOS-specific code
│   └── Windows/                   # Windows-specific code
├── Resources/                     # Shared resources (images, fonts, etc.)
├── App.xaml                       # Application definition
├── AppShell.xaml                  # App navigation shell
├── MainPage.xaml                  # Main UI page
├── MainPage.xaml.cs               # Main page code-behind
├── MauiProgram.cs                 # App startup configuration
└── AzureDeploymentsApp.csproj    # Project file
```

## App Features for Intune Testing

When wrapped with Intune App Wrapper, this app can test:

1. **Data Protection**: App data encryption
2. **Copy/Paste Restrictions**: MAM policy enforcement
3. **PIN Enforcement**: App-level PIN requirements
4. **Conditional Launch**: Device compliance checks
5. **Selective Wipe**: Corporate data removal

## Usage

### Main Screen

- **Azure Deployments Button**: Simulates refreshing Azure deployments
  - Shows loading state
  - Displays success alert
  - Updates timestamp
  
- **Counter Button**: Simple test functionality
  - Increments counter
  - Announces via screen reader

## Development Notes

### VS Code vs Visual Studio

**Yes, you can use VS Code!**

Extensions needed:
- C# Dev Kit
- .NET MAUI extension (optional but helpful)

Build from terminal:
```powershell
dotnet build
```

### Limitations Building from Windows

- **Can build Android** locally on Windows
- **Cannot build iOS** locally without Mac
  - Use GitHub Actions (Free)
  - Or pair to remote Mac via Visual Studio

## Next Steps

1. ✅ App created with requested button
2. ✅ GitHub Actions workflow configured
3. ⏳ Push to GitHub repository
4. ⏳ Configure signing certificates (when available)
5. ⏳ Build IPA via GitHub Actions
6. ⏳ Download IPA artifact
7. ⏳ Wrap with Intune wrapper
8. ⏳ Test on iOS device

## License

Created for Intune testing purposes.

## Support

For questions about:
- **Intune Wrapping**: See `C:\Users\gavishofir\source\Intune-iOS-App\README.md`
- **.NET MAUI**: See [Microsoft MAUI Documentation](https://learn.microsoft.com/dotnet/maui/)
- **GitHub Actions**: See `.github/workflows/build-ios.yml` comments

---

**Created**: November 30, 2025  
**Platform**: .NET 8 MAUI  
**Purpose**: Intune App Wrapper Testing
