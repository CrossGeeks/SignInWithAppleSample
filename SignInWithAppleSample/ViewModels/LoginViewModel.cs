using System;
using System.Windows.Input;
using SignInWithAppleSample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SignInWithAppleSample.ViewModels
{
    public class LoginViewModel
    {
        public bool IsAppleSignInAvailable { get { return appleSignInService?.IsAvailable ?? false; } }
        public ICommand SignInWithAppleCommand { get; set; }

        public event EventHandler OnSignIn = delegate {};

        IAppleSignInService appleSignInService;
        public LoginViewModel()
        {
            appleSignInService = DependencyService.Get<IAppleSignInService>();
            SignInWithAppleCommand = new Command(OnAppleSignInRequest);
        }

        async void OnAppleSignInRequest()
        {

            var account = await appleSignInService.SignInAsync();
            if (account != null)
            {
                Preferences.Set(App.LoggedInKey, true);
                await SecureStorage.SetAsync(App.AppleUserIdKey, account.UserId);
                System.Diagnostics.Debug.WriteLine($"Signed in!\n  Name: {account?.Name ?? string.Empty}\n  Email: {account?.Email ?? string.Empty}\n  UserId: {account?.UserId ?? string.Empty}");
                OnSignIn?.Invoke(this,default(EventArgs));
            }

        }
    }
}
