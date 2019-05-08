using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace Module.Feeds.Domain.Services
{
    public class ContentService
    {
        public static string ReadImageUrl(SyndicationContent content)
        {
            var textContent = content as TextSyndicationContent;

            if (textContent is null)
            {
                return null;
            }

            if (textContent.Type?.ToLower() != "html")
            {
                return null;
            }

            var parser = new HtmlParser();

            var document = parser.ParseDocument(textContent.Text);

            var element = document.QuerySelector("img:first-of-type") as AngleSharp.Html.Dom.IHtmlImageElement;

            return element?.Source;
        }
    }
}
