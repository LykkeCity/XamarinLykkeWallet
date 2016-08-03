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

        public void SetData()
        {
            Task.Run(() =>
            {
                var list = new List<TextCell>();

                var personalData = WalletApiSingleton.Instance.GetPersonalDataAsync().Result;

                var emailCell = new TextCell();
                emailCell.Text = personalData.Email;
                list.Add(emailCell);

                var phoneCell = new TextCell();
                phoneCell.Text = personalData.Phone;
                list.Add(phoneCell);

                var countryCell = new TextCell();
                countryCell.Text = personalData.Country;
                list.Add(countryCell);

                Device.BeginInvokeOnMainThread(() =>
                {
                    personalDataListView.ItemsSource = list;
                });
            });
            
        }
    }
}
