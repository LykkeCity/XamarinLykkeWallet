using System;
using System.Linq;
using Android;
using Android.App;
using Android.Graphics.Drawables;
using Android.Widget;
using App6.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(App6.Droid.IconTabbedRenderer))]
namespace App6.Droid
{
    public class IconTabbedRenderer : TabbedRenderer
    {
        protected override void DispatchDraw(
            global::Android.Graphics.Canvas canvas)
        {
            base.DispatchDraw(canvas);

            SetTabIcons();
        }

        private void SetTabIcons()
        {
            var element = this.Element;
            if (null == element)
            {
                return;
            }

            Activity activity = this.Context as Activity;
            if ((null != activity) && (null != activity.ActionBar) && (activity.ActionBar.TabCount > 0))
            {
                for (int i = 0; i < element.Children.Count; i += 1)
                {
                    var tab = activity.ActionBar.GetTabAt(i);
                    var page = element.Children[i];
                    if ((null != tab) && (null != page))
                    {
                        //int resourceId = this.Context.Resources.GetIdentifier(page.Icon.File, "drawable", this.Context.PackageName);
                        tab.SetIcon(ResourceIdFromString("icdelete"));
                    }
                }
            }
        }
        private int ResourceIdFromString(string name)
        {
            name = name.ToLower()
                .Replace(".png", "")
                .Replace(".jpg", "")
                .Replace(".jpeg", "")
                .Replace(".gif", "")
                .Replace(".ico", "");
            Type type = typeof(Resource.Drawable);
            var a = type.GetFields().Select(x => x.Name.ToLower()).ToList();
            foreach (var p in type.GetFields())
            {
                if (string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))
                    return (int)p.GetValue(null);
            }
            return 0;
        }
    }
}