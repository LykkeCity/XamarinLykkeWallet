using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PasswordHintEntryPage : ContentPage
    {
        private string _email;
        private string _password;

        public PasswordHintEntryPage(string email, string password)
        {
            _email = email;
            _password = password;

            InitializeComponent();
        }

        private void OnHintChanged(object sender, TextChangedEventArgs e)
        {
            submitButton.IsEnabled = !string.IsNullOrEmpty(hintEntry.Text);
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FullNameEntryPage(_email, _password, hintEntry.Text));
        }
    }
}
