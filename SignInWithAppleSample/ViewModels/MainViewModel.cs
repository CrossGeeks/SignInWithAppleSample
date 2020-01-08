using System;
using System.Windows.Input;
using SignInWithAppleSample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SignInWithAppleSample.ViewModels
{
    public class MainViewModel
    {
        public bool IsAppleSignInAvailable { get { return appleSignInService?.IsAvailable ?? false; } }
        public ICommand SignInWithAppleCommand { get; set; }

        IAppleSignInService appleSignInService;
        public MainViewModel()
        {
            appleSignInService = DependencyService.Get<IAppleSignInService>();
            SignInWithAppleCommand = new Command(OnAppleSignInRequest);
        }

        async void OnAppleSignInRequest()
        {

            var account = await appleSignInService.SignInAsync();
            if(account != null)
            {
                await SecureStorage.SetAsync("AppleUserIdKey", account.UserId);
                System.Diagnostics.Debug.WriteLine($"Signed in!\n  Name: {account?.Name ?? string.Empty}\n  Email: {account?.Email ?? string.Empty}\n  UserId: {account?.UserId ?? string.Empty}");

            }

        }
    }
}
