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
        public Page1()
        {
            InitializeComponent();


            var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream("nika");
            img.Source = ImageSource.FromStream(() => { return stream; });
        }

    }
}
