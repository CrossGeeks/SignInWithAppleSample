using System;
using SignInWithAppleSample.Models;
using SignInWithAppleSample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SignInWithAppleSample
{
    public partial class App : Application
    {
        string userId;
        public App()
        {
            InitializeComponent();
            if(Preferences.Get("LoggedIn",false))
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
            
        }

        protected override async void OnStart()
        {
            // Handle when your app starts

            //if (string.IsNullOrEmpty(Settings.Token) || string.IsNullOrEmpty(Settings.AppleExternalAccount)) return;

            var appleSignInService = DependencyService.Get<IAppleSignInService>();
            userId = await SecureStorage.GetAsync("AppleUserIdKey");
            if (appleSignInService.IsAvailable && !string.IsNullOrEmpty(userId))
            {
                
                var credentialState = await appleSignInService.GetCredentialStateAsync(userId);

                switch (credentialState)
                {
                    case AppleSignInCredentialState.Authorized:
                        //Normal app workflow...
                        break;
                    case AppleSignInCredentialState.NotFound:
                    case AppleSignInCredentialState.Revoked:
                        //Logout;
                        SecureStorage.Remove("AppleUserIdKey");
                        Preferences.Set("LoggedIn", false);
                        MainPage = new LoginPage();
                        break;
                }
            }
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
