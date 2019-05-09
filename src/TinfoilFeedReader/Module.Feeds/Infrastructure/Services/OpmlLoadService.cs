using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Module.Feeds.Domain;

namespace Module.Feeds.Infrastructure.Services
{
    public class OpmlLoadService
    {
        public Feed LoadFeedFrom(string opml)
        {
            var document = XDocument.Parse(opml);

            var root = document.Element("opml");
            var body = root?.Element("body");
            var outline = body?.Element("outline");
            var title = outline?.Attribute("title");

            if (title is null)
            {
                return null;
            }

            return new Feed
            {
                Name = title?.Value,
                Sources = InspectOutlines(outline)
            };
        }

        private ICollection<FeedSource> InspectOutlines(XElement outline)
        {
            var feedSources = new List<FeedSource>();

            if (outline.HasElements)
            {
                var nestedOutlines = outline.Elements("outline");

                foreach (var nestedOutline in nestedOutlines)
                {
                    var childrenSources = InspectOutlines(nestedOutline);
                    feedSources.AddRange(childrenSources);
                }
            }
            else
            {
                var type = outline.Attribute("type")?.Value;

                var title = outline.Attribute("title")?.Value;
                var url = outline.Attribute("xmlUrl")?.Value;

                if (type == "rss" && title is object && url is object)
                {
                    var feedSource = new FeedSource
                    {
                        Name = title,
                        Url = url
                    };

                    feedSources.Add(feedSource);
                }
            }

            return feedSources;
        }
    }
}
