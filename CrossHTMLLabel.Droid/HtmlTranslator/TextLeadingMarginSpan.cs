using System;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using JObject = Java.Lang.Object;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
    public class TextLeadingMarginSpan : JObject, ILeadingMarginSpan
    {
        private readonly int _margin;
        private readonly string _prefix;

        public TextLeadingMarginSpan(int margin, string prefix)
        {
            _margin = margin;
            _prefix = prefix;
        }

        public void DrawLeadingMargin(Canvas c, Paint p, int x, int dir, int top, int baseline, int bottom, Java.Lang.ICharSequence text, int start, int end, bool first, Layout layout)
        {
            var startCharOfSpan = ((ISpanned)text).GetSpanStart(this);
            var isFirstCharacter = startCharOfSpan == start;

            if (!isFirstCharacter)
            {
                return;
            }

            c.DrawText(_prefix, x, baseline, p);
        }

        public int GetLeadingMargin(bool first)
        {
            return _margin;
        }

        protected TextLeadingMarginSpan(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
