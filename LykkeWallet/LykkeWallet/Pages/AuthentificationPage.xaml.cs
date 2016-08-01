using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using Xamarin.Forms;
using LykkeWallet.LocalKeyStorageAccess;

namespace LykkeWallet.Pages
{
    public partial class AuthentificationPage : ContentPage
    {
        private string _email;
        private AuthentificationPageViewModel ViewModel => authentificationPageViewModel;
        public AuthentificationPage(string _email)
        {
            InitializeComponent();
            ViewModel.Email = _email;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            passwordEntry.Focus();
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;

            try
            {
                var result = await WalletApiSingleton.Instance.AuthAsync(ViewModel.Email, ViewModel.Password);
                LocalKeyAccessSingleton.Instance.SavePrivateKey(result.EncodedPrivateKey);
                LocalKeyAccessSingleton.Instance.AddOrUpdateEmailKeyPair(ViewModel.Email, result.EncodedPrivateKey);
                
                if (!string.IsNullOrEmpty(result.EncodedPrivateKey))
                {

                }
                await Navigation.PushAsync(new MainTabbedPage(true));
            }
            catch (InvalidUsernameOrPasswordException ex)
            {
                await DisplayAlert("", "Invalid username or password", "OK");
                passwordEntry.Focus();
            }
            finally
            {
                submitButton.Clicked += OnSubmitButtonClicked;
            }

        }
    }
}
