using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PersonalDataInfoPage : ContentPage
    {
        public PersonalDataInfoPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var personalData = await WalletApiSingleton.Instance.GetPersonalDataAsync();

            var tableData = new TableView();

            var emailCell = new TextCell();
            emailCell.Text = personalData.Email;

            var phoneCell = new TextCell();
            phoneCell.Text = personalData.Phone;

            var countryCell = new TextCell();
            countryCell.Text = personalData.Country;

            tableData.Root.Add(new TableSection());
            tableData.Root.LastOrDefault().Add(emailCell);
            tableData.Root.LastOrDefault().Add(phoneCell);
            tableData.Root.LastOrDefault().Add(countryCell);


            Content = tableData;

            base.OnAppearing();
            
        }
    }
}
