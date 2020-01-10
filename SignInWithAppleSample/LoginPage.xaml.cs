using System;
using System.Collections.Generic;
using SignInWithAppleSample.ViewModels;
using Xamarin.Forms;

namespace SignInWithAppleSample
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var vm = new LoginViewModel();
            vm.OnSignIn += OnSignIn;
            BindingContext = vm;
            InitializeComponent();
        }

        private void OnSignIn(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}
