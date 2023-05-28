using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace tgbot_synoptyk.Parsing
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
