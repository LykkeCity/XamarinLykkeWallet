using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class Page1 : ContentPage
    {
        private Image imgCode;
        private Button btnCreate;
        private EntryCell txtBarcode;
        public Page1()
        {
            InitializeComponent();
            btnCreate = new Button { Text = "Create" };
            imgCode = new Image();
            txtBarcode = new EntryCell { Label = "Bar Code" };

            btnCreate.Clicked += OnSubmitButtonClicked;

            this.Content = new StackLayout
            {
                Children = {
                    btnCreate,
                    imgCode,
                    new TableView(new TableRoot {
                        new TableSection {
                            txtBarcode
                        }
                    })
                }
            };
        }
        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {

        }

    }
}
