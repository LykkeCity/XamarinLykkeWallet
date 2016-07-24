using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LykkeWallet.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(LabelFontRenderer))]
namespace LykkeWallet.Droid
{
    public class LabelFontRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var label = (TextView)Control; // for example
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Roboto-Regular.ttf");  // font name specified here
            label.Typeface = font;
        }
    }
}