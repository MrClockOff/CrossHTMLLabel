using System;
using Android.Runtime;
using JObject = Java.Lang.Object;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
    public class NumberListItemMark : JObject, IMarkSpan
    {
        private readonly int _leadingMargin;

        public int Index { get; }

        public NumberListItemMark(int leadingMargin, int index)
        {
            _leadingMargin = leadingMargin;
            Index = index;
        }

        public JObject CreateStyleSpan()
        {
            return new TextLeadingMarginSpan(_leadingMargin, $"{Index}.");
        }

        protected NumberListItemMark(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }
    }
}
