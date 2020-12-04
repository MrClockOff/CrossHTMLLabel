using Foundation;
using UIKit;

namespace CrossHTMLLabel.Extensions
{
    public static class NSMutableAttributedStringExtenstions
    {
        public static void ReplaceFont(this NSMutableAttributedString attributedString, UIFont font, bool includingFontSize = false)
        {
            //https://stackoverflow.com/a/47320125/3938174
            var fullRange = new NSRange(0, attributedString.Length);

            attributedString.BeginEditing();
            attributedString.EnumerateAttribute(UIStringAttributeKey.Font, fullRange, NSAttributedStringEnumeration.LongestEffectiveRangeNotRequired,
                (NSObject value, NSRange range, ref bool stop) =>
                {
                    if (value is UIFont currentFont)
                    {
                        var newFontDescriptor = currentFont.FontDescriptor
                        .CreateWithFamily(font.FamilyName)
                        .CreateWithTraits(currentFont.FontDescriptor.SymbolicTraits);
                        var newFont = UIFont.FromDescriptor(newFontDescriptor, includingFontSize ? font.PointSize : currentFont.PointSize);

                        attributedString.RemoveAttribute(UIStringAttributeKey.Font, range);
                        attributedString.AddAttribute(UIStringAttributeKey.Font, newFont, range);
                    }
                });
            attributedString.EndEditing();
        }        
    }
}
