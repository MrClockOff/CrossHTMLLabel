using System;
using Android.Runtime;
using JObject = Java.Lang.Object;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
    public class BulletListItemMark : JObject, IMarkSpan
    {
        private const string Prefix = "•";
        private readonly int _leadingMargin;

        public BulletListItemMark(int leadingMargin)
        {
            _leadingMargin = leadingMargin;
        }

        public JObject CreateStyleSpan()
        {
            return new TextLeadingMarginSpan(_leadingMargin, Prefix);
        }

        protected BulletListItemMark(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
