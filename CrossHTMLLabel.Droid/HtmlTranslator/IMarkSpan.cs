using Android.Runtime;
using Java.Interop;
using JObject = Java.Lang.Object;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
    public interface IMarkSpan : IJavaObject, IJavaPeerable
    {
        JObject CreateStyleSpan();
    }
}
