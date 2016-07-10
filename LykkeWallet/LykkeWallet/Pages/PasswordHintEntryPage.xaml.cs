using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PasswordHintEntryPage : ContentPage
    {
        private readonly string _email;
        private readonly string _password;

        public PasswordHintEntryPage(string email, string password)
        {
            _email = email;
            _password = password;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            hintEntry.Focus();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            await WalletApiSingleton.Instance.RegisterAccount(_email, "", "", _password, hintEntry.Text, "");

            Navigation.PushAsync(new FullNameEntryPage());
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if(submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }
    }
}
