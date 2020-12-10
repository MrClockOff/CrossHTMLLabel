using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Text.Method;
using Android.Text.Style;
using Android.Text.Util;
using Android.Views;
using Android.Widget;
using CrossHTMLLabel.Droid.Extensions;
using Java.Lang;
using Org.Sufficientlysecure.Htmltextview;

namespace CrossHTMLLabel.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const string HtmlString = "<p>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</p><p><br></p><ul><li>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</li><li>item 2</li><li>item 3</li></ul><p><br></p><ol><li>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</li><li>Item2</li><li>Item3</li></ol><p><br></p><p><center><b>Bold</b></center></p><p><br></p><h2><strong>Heading</strong></h2><p><br></p><p><u>Underline</u></p><p><br></p><p><em>Italic</em></p><p><br></p><p><a href=\"https://news.bbc.co.uk\" target=\"_blank\"><em>link</em></a></p><p><br></p><p><a href=\"mailto:sam.smith@mhub.tv\" target=\"_blank\"><em>mailto</em></a></p>";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            SetupView();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region CrossHTMLLabel implementation
        private void SetupView()
        {
            var textView = FindViewById<TextView>(Resource.Id.TextView);
            textView.AutoLinkMask = MatchOptions.WebUrls;
            textView.MovementMethod = LinkMovementMethod.Instance;

            if (string.IsNullOrEmpty(HtmlString))
            {
                textView.TextFormatted = string.Empty.ToHtmlSpanned();
                return;
            }

            //var spanned = HtmlString.ToHtmlSpanned();
            var spannable = HtmlFormatter.FormatHtml(new HtmlFormatterBuilder().SetHtml(HtmlString)) as SpannableStringBuilder;
            var links = spannable.GetSpans(0, spannable.Length(), Java.Lang.Class.FromType(typeof(URLSpan)));

            TrimTrailingWhitespace(spannable);
            textView.TextFormatted = spannable;

            var builder = textView.TextFormatted as ISpannable;

            foreach (var link in links.OfType<URLSpan>())
            {
                builder.SetSpan(link, spannable.GetSpanStart(link), spannable.GetSpanEnd(link), SpanTypes.PointPoint);
            }

            var spans = builder.GetSpans(0, builder.Length(), Java.Lang.Class.FromType(typeof(URLSpan)));

            foreach (var span in spans.OfType<URLSpan>())
            {
                MakeLinkClickable(builder, span);
            }
        }

        private static void TrimTrailingWhitespace(SpannableStringBuilder source)
        {
            if (source == null && source.Length() <= 0)
            {
                return;
            }

            int i = source.Length();

            // loop back to the first non-whitespace character
            while (--i >= 0 && Character.IsWhitespace(source.CharAt(i)))
            {
            }

            source.Delete(i, source.Length() - 1);
        }

        private static void MakeLinkClickable(ISpannable spannable, URLSpan span)
        {
            var start = spannable.GetSpanStart(span);
            var end = spannable.GetSpanEnd(span);
            var flags = spannable.GetSpanFlags(span);
            var clickableLink = new ClickableLinkSpan(span.URL);
            spannable.SetSpan(clickableLink, start, end, flags);
            spannable.RemoveSpan(span);
        }

        /// <summary>
        /// ClickableLinkSpan implementation
        /// </summary>
        private class ClickableLinkSpan : ClickableSpan
        {
            private readonly string _url;

            /// <summary>
            /// Span constructor
            /// </summary>
            /// <param name="url"></param>
            public ClickableLinkSpan(string url)
            {
                _url = url;
            }

            /// <summary>
            /// OnClick handler
            /// </summary>
            /// <param name="widget"></param>
            public override void OnClick(Android.Views.View widget)
            {
                //Mvx.IoCProvider.Resolve<IPlatformServices>().OpenLink(_url, null);
                System.Diagnostics.Debug.WriteLine($"Link Click: {_url}");
            }
        }
        #endregion
    }
}