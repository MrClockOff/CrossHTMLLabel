using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Runtime;
using Android.Text;
using Org.Xml.Sax;
using static Android.Text.Html;
using JObject = Java.Lang.Object;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
	/// <summary>
	/// https://medium.com/swlh/making-nested-lists-with-android-spannables-in-kotlin-4ad00052912c
	/// </summary>
	public class TagHandler : JObject, ITagHandler
	{
		private const int LeadingMargin = 50;
		public const string OL_Tag = "orderedlist";
		public const string UL_Tag = "unorderedlist";
		public const string LI_Tag = "listitem";

		private readonly Stack<IListTag> _lists = new Stack<IListTag>();

		public TagHandler()
        {
        }

		public void HandleTag(bool opening, string tag, IEditable output, IXMLReader xmlReader)
		{
			switch (tag)
            {
				case UL_Tag:
					if (opening)
                    {
						_lists.Push(new UlTag(LeadingMargin));
                    }
					else
                    {
						_lists.Pop();
                    }
					break;
				
				case OL_Tag:
					if (opening)
					{
						_lists.Push(new OlTag(LeadingMargin));
					}
					else
					{
						_lists.Pop();
					}
					break;
				case LI_Tag:
					if (opening)
					{
						_lists.Peek().OpenItem(output);
					}
					else
					{
						_lists.Peek().CloseItem(output);
					}
					break;
				default:
					Debug.WriteLine($"Unknow HTML tag: {tag}");
					break;
			}			
		}

		protected TagHandler(IntPtr javaReference, JniHandleOwnership transfer)
				: base(javaReference, transfer)
        {
        }
	}
}