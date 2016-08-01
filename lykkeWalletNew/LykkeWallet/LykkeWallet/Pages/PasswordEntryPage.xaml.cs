using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.LocalKeyStorageAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PasswordEntryPage : ContentPage
    {
        private readonly string _email;
        public PasswordEntryPage(string email)
        {
            InitializeComponent();
            _email = email;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            passwordEntry.Focus();
        }
        
        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;
            var localStorage = new LocalKeyStorage();
            localStorage.Save(Constants.PASSWORD, passwordEntry.Text);
            await Navigation.PushAsync(new PasswordHintEntryPage(_email, passwordEntry.Text));
            submitButton.Clicked += OnSubmitButtonClicked;
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);

        }
    }
}