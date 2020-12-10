using System;
using System.Linq;
using Android.Text;
using JObject = Java.Lang.Object;
using JClass = Java.Lang.Class;
using Android.Text.Style;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
	public abstract class ListTagBase : IListTag
	{
		public virtual void OpenItem(IEditable text)
		{
			AppendNewLine(text);
			SetStartMarkSpan(text, CreateStartMarkSpan());
		}

		public virtual void CloseItem(IEditable text)
		{
			//AppendNewLine(text);			
			SetSpanFromMarkSpan(text);
		}

		protected virtual void AppendNewLine(IEditable text)
		{
			if (text.Length() <= 0)
			{
				return;
			}

			if (text.CharAt(text.Length() - 1).Equals(Environment.NewLine))
			{
				return;
			}

			text.Append(Environment.NewLine);
		}

		protected abstract IMarkSpan CreateStartMarkSpan();

		protected virtual void SetStartMarkSpan(IEditable text, IMarkSpan markSpan)
		{
			var currentPosition = text.Length();
			text.SetSpan((JObject)markSpan, currentPosition, currentPosition, SpanTypes.MarkMark);
		}

		protected virtual IMarkSpan GetLastMarkSpan(IEditable text)
		{
			var markSpanType = CreateStartMarkSpan().GetType();
			var markSpans = text.GetSpans(0, text.Length(), JClass.FromType(markSpanType)).ToList();
			var lastMarkSpan = markSpans.LastOrDefault();

			return lastMarkSpan as IMarkSpan;
		}

		protected virtual void SetSpanFromMarkSpan(IEditable text)
		{
			var markSpan = GetLastMarkSpan(text);

			if (markSpan == null)
			{
				return;
			}

			var styleSpan = markSpan.CreateStyleSpan();
			var markStartLocation = text.GetSpanStart((JObject)markSpan);
			var markEndLocation = text.Length();
			text.RemoveSpan((JObject)markSpan);

			if (markStartLocation == markEndLocation)
			{
				return;
			}

			text.SetSpan(styleSpan, markStartLocation, markEndLocation, SpanTypes.ExclusiveExclusive);
		}
	}
}
