using Foundation;

namespace CrossHTMLLabel.Extensions
{
    public static class NSStringExtensions
    {
        public static NSAttributedString ToAttributedString(this NSString source)
        {
            try
            {
                NSError error = null;
                var data = NSData.FromString(source, NSStringEncoding.UTF8);
                var attributes = new NSAttributedStringDocumentAttributes
                {
                    DocumentType = NSDocumentType.HTML,
                    StringEncoding = NSStringEncoding.UTF8,

                };                
                var attributedString = new NSAttributedString(data, attributes, ref error);

                if (error != null)
                {
                    return new NSAttributedString();
                }

                return attributedString;
            }
            catch
            {
                return new NSAttributedString();
            }
        }

        public static NSMutableAttributedString ToMutableAttributedString(this NSString source)
        {
            var attributedString = ToAttributedString(source);
            var mutableAttributedString = new NSMutableAttributedString(attributedString);

            return mutableAttributedString;
        }
    }
}
