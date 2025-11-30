# Next Steps: Deploying AzureDeploymentsApp to GitHub and Building IPA

## ✅ What We've Completed

1. ✅ Installed .NET 8 SDK
2. ✅ Installed MAUI workload
3. ✅ Created `AzureDeploymentsApp` MAUI project
4. ✅ Customized UI with "Click here to refresh Azure deployments page" button
5. ✅ Created GitHub Actions workflow for iOS builds
6. ✅ Initialized git repository with all files committed

## 📁 Project Location

Your app is here: `C:\Users\gavishofir\source\AzureDeploymentsApp`

## 🎯 Next Steps to Build iOS IPA

### Step 1: Create GitHub Repository

**Option A: Using GitHub Web Interface**

1. Go to [github.com](https://github.com)
2. Click "+" → "New repository"
3. Repository name: `AzureDeploymentsApp` (or your choice)
4. Choose Public or Private
5. **DO NOT** initialize with README (we already have one)
6. Click "Create repository"

**Option B: Using GitHub CLI (if installed)**

```powershell
# Install GitHub CLI (if not already installed)
winget install GitHub.cli

# Login to GitHub
gh auth login

# Create repository
gh repo create AzureDeploymentsApp --public --source=. --remote=origin --push
```

### Step 2: Push Code to GitHub

After creating the repository on GitHub, you'll see commands like this:

```powershell
cd C:\Users\gavishofir\source\AzureDeploymentsApp

# Add remote repository
git remote add origin https://github.com/YOUR-USERNAME/AzureDeploymentsApp.git

# Push code
git branch -M main
git push -u origin main
```

**Replace `YOUR-USERNAME`** with your GitHub username.

### Step 3: Verify GitHub Actions Started

1. Go to your repository on GitHub
2. Click the "Actions" tab
3. You should see "Build iOS IPA" workflow running
4. Click on the workflow run to watch progress

**Expected:** The workflow will build the iOS project, but won't create a signed .ipa yet (we need signing certificates for that).

### Step 4: View Build Artifacts

Once the GitHub Action completes:

1. Click on the completed workflow run
2. Scroll down to "Artifacts" section
3. Download `ios-build` artifact
4. Extract and explore the built iOS app files

## 🔐 To Create Signed IPA (Required for Intune Wrapping)

### Prerequisites You'll Need

**From Apple Developer Account:**

1. **Distribution Certificate** (.p12 file)
   - Go to [developer.apple.com](https://developer.apple.com)
   - Certificates, Identifiers & Profiles
   - Create iOS Distribution certificate
   - Download and export as .p12

2. **Provisioning Profile** (.mobileprovision)
   - Create App ID for your app
   - Create Distribution provisioning profile
   - Download .mobileprovision file

### Adding Secrets to GitHub

1. In your GitHub repository, go to Settings → Secrets and variables → Actions
2. Click "New repository secret"

**Add these three secrets:**

**Secret 1: IOS_CERTIFICATE_BASE64**
```powershell
# Convert .p12 to base64 (on Windows)
$certBytes = [System.IO.File]::ReadAllBytes("C:\path\to\certificate.p12")
$certBase64 = [System.Convert]::ToBase64String($certBytes)
$certBase64 | Set-Clipboard
# Now paste into GitHub secret
```

**Secret 2: IOS_CERTIFICATE_PASSWORD**
```
The password you set when exporting the .p12 certificate
```

**Secret 3: IOS_PROVISIONING_PROFILE_BASE64**
```powershell
# Convert .mobileprovision to base64
$profileBytes = [System.IO.File]::ReadAllBytes("C:\path\to\profile.mobileprovision")
$profileBase64 = [System.Convert]::ToBase64String($profileBytes)
$profileBase64 | Set-Clipboard
# Now paste into GitHub secret
```

### Enabling Signed Builds

1. Open `.github/workflows/build-ios.yml` in your project
2. Uncomment the sections marked with:
   - `# Uncomment and configure these steps when you have signing certificates:`
3. Update the provisioning profile name in the build command
4. Commit and push changes
5. GitHub Actions will now build a signed .ipa!

## 📦 Downloading and Wrapping the IPA

Once you have a signed .ipa built by GitHub Actions:

### Step 1: Download IPA from GitHub

1. Go to GitHub Actions tab
2. Click on the successful workflow run
3. Download `ios-app-ipa` artifact
4. Extract the .ipa file

### Step 2: Wrap with Intune Wrapper

```powershell
# Navigate to Intune wrapper workspace
cd C:\Users\gavishofir\source\Intune-iOS-App

# Download Intune wrapper tool (if not already done)
git clone https://github.com/msintuneappsdk/intune-app-wrapping-tool-ios.git

# Wrap the app
.\scripts\wrap-app.ps1 `
    -InputIPA "C:\path\to\downloaded\AzureDeploymentsApp.ipa" `
    -OutputIPA "C:\path\to\output\AzureDeploymentsApp-wrapped.ipa" `
    -ProvisioningProfile "C:\path\to\distribution.mobileprovision" `
    -SigningIdentity "iPhone Distribution: Your Company (TEAMID)"

# Verify wrapped app
.\scripts\verify-wrapper.ps1 -WrappedIPA "C:\path\to\output\AzureDeploymentsApp-wrapped.ipa"
```

### Step 3: Deploy to Intune

1. Sign in to [endpoint.microsoft.com](https://endpoint.microsoft.com)
2. Apps → iOS/iPadOS → Add
3. Select "Line-of-business app"
4. Upload wrapped .ipa
5. Configure app protection policies
6. Assign to test users

## 🧪 Testing Without Certificates (Current State)

**Right now, without Apple certificates, you can:**

✅ Build the project to verify it compiles
✅ Download build artifacts to inspect
✅ Test the workflow automation
✅ Practice the entire process
❌ Cannot create installable .ipa yet

**The GitHub Actions workflow is running successfully**, it just needs signing certificates to create the final .ipa.

## 💡 Alternative: Use Customer's Certificates

If you're helping a customer, they likely already have:
- Apple Developer account
- Distribution certificates
- Provisioning profiles

**You can:**
1. Ask customer to provide these (securely)
2. Add to GitHub secrets
3. Build signed .ipa
4. Or have customer build and send you the .ipa

## 🆚 VS Code vs Visual Studio

**Great news: VS Code works perfectly!**

What you did was entirely in VS Code and PowerShell:
- ✅ Created MAUI project via CLI
- ✅ Edited files in VS Code
- ✅ Set up GitHub Actions
- ✅ No Visual Studio needed for this workflow!

**Visual Studio is only needed if:**
- You want GUI designer for XAML
- You want to pair directly to Mac build host
- You prefer IDE over command line

**Your approach (VS Code + GitHub Actions) is actually more DevOps-friendly!**

## 📋 Quick Reference Commands

```powershell
# View project status
cd C:\Users\gavishofir\source\AzureDeploymentsApp
git status

# Build locally for Android (to test)
dotnet build -f net8.0-android -c Release

# Create new branch for changes
git checkout -b feature/add-functionality

# Commit and push changes
git add .
git commit -m "Add new feature"
git push origin feature/add-functionality
```

## 🎯 Summary

**What you have now:**
- ✅ Working MAUI app with custom Azure Deployments UI
- ✅ GitHub Actions workflow for automated iOS builds
- ✅ Complete project ready to push to GitHub
- ✅ All documentation and configuration files

**What you need for production .ipa:**
- ⏳ GitHub account (to push code)
- ⏳ Apple Developer account (for certificates)
- ⏳ Distribution certificate and provisioning profile

**Once you have those, the entire build → wrap → deploy pipeline is automated!**

---

## 🚀 Ready to Proceed?

Run these commands when you're ready to push to GitHub:

```powershell
cd C:\Users\gavishofir\source\AzureDeploymentsApp

# Configure git identity (one-time)
git config user.name "Your Name"
git config user.email "your.email@example.com"

# Add GitHub remote (replace YOUR-USERNAME)
git remote add origin https://github.com/YOUR-USERNAME/AzureDeploymentsApp.git

# Push to GitHub
git branch -M main
git push -u origin main
```

Then watch the magic happen in GitHub Actions! 🎉

---

**Created:** November 30, 2025  
**Location:** `C:\Users\gavishofir\source\AzureDeploymentsApp`
