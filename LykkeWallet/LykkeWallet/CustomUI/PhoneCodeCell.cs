using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeWallet.CustomUI
{
    public class PhoneCodeCell : ViewCell
    {
        private Label _labelLeft, _labelRight;

        public static readonly BindableProperty CountryProperty = BindableProperty.Create("Country", typeof(string), typeof(PhoneCodeCell), "Country");

        public static readonly BindableProperty CodeProperty = BindableProperty.Create("Code", typeof(string), typeof(PhoneCodeCell), "Code");

        public string Country
        {
            get { return (string)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        public string Code
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }


        public PhoneCodeCell()
        {
            _labelLeft = new Label { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
            //labelLeft.SetBinding(Label.TextProperty, "Country");
            _labelRight = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
            //labelRight.SetBinding(Label.TextProperty, "Code");

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 0, 10, 0)
            };

            horizontalStack.Children.Add(_labelLeft);
            horizontalStack.Children.Add(_labelRight);

            View = horizontalStack;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                _labelLeft.Text = Country;
                _labelRight.Text = Code;
            }
        }

    }
}
