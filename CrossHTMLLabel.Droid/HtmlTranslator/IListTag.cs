using Android.Text;

namespace CrossHTMLLabel.Droid.HtmlTranslator
{
	public interface IListTag
	{
		void OpenItem(IEditable text);
		void CloseItem(IEditable text);
	}
}
