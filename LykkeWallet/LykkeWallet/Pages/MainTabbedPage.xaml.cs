using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(bool requirePin)
        {
            if (requirePin)
            {
                Navigation.PushModalAsync(new PinEntryPage());
            }
            InitializeComponent();
        }
    }
}
