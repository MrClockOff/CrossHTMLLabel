using System.Text.RegularExpressions;
using Android.OS;
using Android.Support.V4.Text;
using Android.Text;
using CrossHTMLLabel.Droid.HtmlTranslator;

namespace CrossHTMLLabel.Droid.Extensions
{
    public static class StringExtensions
    {
        public static ISpanned ToHtmlSpanned(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new SpannableString(string.Empty);
            }

            if (Build.VERSION.SdkInt < BuildVersionCodes.N)
            {
                // FromHtmlOptions are ignored on devices with API less than 24
                // so excessive break lines has to be cleared out.
                source = Regex.Replace(source, "<p><br></p>", "<p></p>");
                source = Regex.Replace(source, "</p><p>", "<br>");
            }

            // Depending on the version of Android API there is no support for
            // list HTML markup or there is partial incomplete support.
            // To override partial support of HTML lists we have to replace ul,
            // ol and li tags with our custom. This will trigger use of custom
            // TagHandler.
            // https://medium.com/swlh/making-nested-lists-with-android-spannables-in-kotlin-4ad00052912c
            var formattedSource = FormatHtml(source);
            var fromHtmlOptions = FromHtmlOptions.ModeCompact;
            return HtmlCompat.FromHtml(formattedSource, (int)fromHtmlOptions, null, new TagHandler());
        }

        
        private static string FormatHtml(string source)
        {
            var result = Regex.Replace(source, "(?i)<ul[^>]*>", $"<{TagHandler.UL_Tag}>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "(?i)</ul>", $"</{TagHandler.UL_Tag}>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "(?i)<ol[^>]*>", $"<{TagHandler.OL_Tag}>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "(?i)</ol>", $"</{TagHandler.OL_Tag}>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "(?i)<li[^>]*>", $"<{TagHandler.LI_Tag}>", RegexOptions.IgnoreCase);
            result = Regex.Replace(result, "(?i)</li>", $"</{TagHandler.LI_Tag}>", RegexOptions.IgnoreCase);
            return result;
        }
    }
}
