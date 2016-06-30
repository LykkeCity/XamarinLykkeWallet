using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class FullNameEntryPage : ContentPage
    {
        private string _email;
        private string _password;
        private string _passhint;
        public FullNameEntryPage(string email, string password, string passhint)
        {
            _email = email;
            _password = password;
            _passhint = passhint;

            InitializeComponent();
        }

        private void OnFullNameChanged(object sender, TextChangedEventArgs e)
        {
            submitButton.IsEnabled = !string.IsNullOrEmpty(fullNameEntry.Text);
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PhoneEntryPage());
        }
    }
}
