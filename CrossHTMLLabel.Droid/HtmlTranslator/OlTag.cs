namespace CrossHTMLLabel.Droid.HtmlTranslator
{
	public class OlTag : ListTagBase
	{
		private readonly int _leadingMargin;
		private int _index = 1;

		public OlTag(int leadingMargin)
        {
			_leadingMargin = leadingMargin;
        }

		protected override IMarkSpan CreateStartMarkSpan()
		{
			return new NumberListItemMark(_leadingMargin, ++_index);
		}
	}
}
