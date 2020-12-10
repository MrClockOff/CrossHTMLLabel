namespace CrossHTMLLabel.Droid.HtmlTranslator
{
    public class UlTag : ListTagBase
    {
        private readonly int _leadingMargin;

        public UlTag(int leadingMargin)
        {
            _leadingMargin = leadingMargin;
        }

        protected override IMarkSpan CreateStartMarkSpan()
        {
            return new BulletListItemMark(_leadingMargin);
        }
    }
}
