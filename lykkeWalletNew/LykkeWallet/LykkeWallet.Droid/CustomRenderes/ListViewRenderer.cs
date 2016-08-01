using System;
using LykkeWallet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(LykkeWallet.Droid.ListtViewCustomRenderer))]
namespace LykkeWallet.Droid
{
    public class ListtViewCustomRenderer : ListViewRenderer
    {

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            this.Control.SetSelection(0);
        }

        public ListtViewCustomRenderer()
        {
        }
    }
}