using System;
using SignInWithAppleSample.Models;
using SignInWithAppleSample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SignInWithAppleSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts

            //if (string.IsNullOrEmpty(Settings.Token) || string.IsNullOrEmpty(Settings.AppleExternalAccount)) return;

            var appleSignInService = DependencyService.Get<IAppleSignInService>();
            if(appleSignInService.IsAvailable)
            {
                var userId= await SecureStorage.GetAsync("AppleUserIdKey");
                var credentialState = await appleSignInService.GetCredentialStateAsync(userId);

                switch (credentialState)
                {
                    case AppleSignInCredentialState.Authorized:
                        //Normal app workflow...
                        break;
                    case AppleSignInCredentialState.NotFound:
                    case AppleSignInCredentialState.Revoked:
                        //Logout;
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
