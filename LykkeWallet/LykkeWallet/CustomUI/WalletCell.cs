using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Converters;
using Xamarin.Forms;
using static System.Decimal;

namespace LykkeWallet.CustomUI
{
    public class WalletCell : ViewCell
    {
        private Label _labelLeft, _labelRight, _labelRightRight;

        public static readonly BindableProperty CodeProperty = BindableProperty.Create("Code", typeof(string), typeof(WalletCell), "Code");
        public static readonly BindableProperty SymbolProperty = BindableProperty.Create("Symbol", typeof(string), typeof(WalletCell), "Symbol");
        public static readonly BindableProperty BalanceProperty = BindableProperty.Create("Balance", typeof(decimal), typeof(WalletCell), 0m);

        public string Code
        {
            set { _labelLeft.Text = value; }
            get { return _labelLeft.Text; }
        }
        public string Symbol
        {
            set { _labelRight.Text = value; }
            get { return _labelRight.Text; }
        }
        public decimal Balance
        {
            set { _labelRightRight.Text = value.ToString(); }
            get { return decimal.Parse(_labelRightRight.Text);  }
        }

        public WalletCell(/*string s, string symbol, decimal balance*/)
        { 
            _labelLeft = new Label { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            _labelRight = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            _labelRightRight = new Label { HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };


            _labelLeft.SetBinding(Label.TextProperty, new Binding("Code"));
            _labelRight.SetBinding(Label.TextProperty, new Binding("Symbol"));
            _labelRightRight.SetBinding(Label.TextProperty, new Binding("Balance"));

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 40,
                Padding = new Thickness(10, 0, 10, 0),
            };

            horizontalStack.Children.Add(_labelLeft);
            horizontalStack.Children.Add(_labelRight);
            horizontalStack.Children.Add(_labelRightRight);

            View = horizontalStack;
        }
    }
}
