using System;
using System.Text.RegularExpressions;
using CrossHTMLLabel.Extensions;
using Foundation;
using UIKit;

namespace CrossHTMLLabel
{
    public partial class ViewController : UIViewController
    {
        private const string HtmlString = "<p><strong><em>Bold Italics text</em></strong></p>"; //" < p>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</p><p><br></p><ul><li>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</li><li>item 2</li><li>item 3</li></ul><p><br></p><ol><li>The R number, which is the number of people that one infected person will pass on the virus to, is expected to fall below one, she says - a sign that restrictions on people's lives are having an effect.</li><li>Item2</li><li>Item3</li></ol><p><br></p><p><b>Bold</b></p><p><br></p><h2><strong>Heading</strong></h2><p><br></p><p><u>Underline</u></p><p><br></p><p><em>Italic</em></p><p><br></p><p><a href=\"https://news.bbc.co.uk\" target=\"_blank\"><em>link</em></a></p><p><br></p><p><a href=\"mailto:sam.smith@mhub.tv\" target=\"_blank\"><em>mailto</em></a></p>";
        private string CssStyle =>
                $"<style>" +
                $"body {{margin: 0; font-size: {Label.Font.PointSize} px;}} " +
                $"p {{margin: 0;}} " +
                $"h1,h2,h3,h4,h5,h6 {{margin: 0; padding: 0;}}" +
                $"</style>";
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            SetupView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void SetupView()
        {
            var htmlStringMod = Regex.Replace(HtmlString ?? string.Empty, "<p><br>? ?\\/?>?<\\/p>", "<p></p>");
            var styledHtmlString = $"{htmlStringMod}{CssStyle}";
            try
            {
                var attributedString = new NSString(styledHtmlString).ToMutableAttributedString();

                // Do not include font size replacement as the size is specified
                // by CSS style. Specifying size by CSS will set different text
                // sizes for tags like H1, H2, H3...
                attributedString.ReplaceFont(Label.Font, false);
                Label.AttributedText = attributedString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}