using Foundation;
#if IOS
using Microsoft.Intune.MAM;
#endif

namespace AzureDeploymentsApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
#if IOS
	public override bool FinishedLaunching(UIKit.UIApplication application, NSDictionary launchOptions)
	{
		// Initialize OLD Intune SDK (version 18.x)
		// This simulates a customer's legacy app that needs wrapping with new wrapper
		IntuneMAMEnrollmentManager.Instance.LoginAndEnrollAccount(null);
		
		return base.FinishedLaunching(application, launchOptions);
	}
#endif

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
