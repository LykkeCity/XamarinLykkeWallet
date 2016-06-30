using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PasswordEntryPage : ContentPage
    {
        private string _email;
        public PasswordEntryPage(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void OnPasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordEntry.Text == null || passwordRepeatEntry.Text == null)
            {
                submitButton.IsEnabled = false;
                return;
            }
            else if (passwordEntry.Text.Length < 6 || passwordRepeatEntry.Text.Length < 6)
            {
                submitButton.IsEnabled = false;
                return;
            }
            else if (passwordEntry.Text != passwordRepeatEntry.Text)
            {
                submitButton.IsEnabled = false;
                return;
            }

            submitButton.IsEnabled = true;
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PasswordHintEntryPage(_email, passwordEntry.Text));
        }
    }
}