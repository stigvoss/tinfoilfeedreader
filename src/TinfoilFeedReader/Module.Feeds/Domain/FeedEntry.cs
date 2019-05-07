using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Module.Feeds.Domain
{
    public struct FeedEntry
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string SourcePage => new Uri(Link).Host;

        public DateTimeOffset PublishDate { get; set; }

        public IEnumerable<string> Authors { get; set; }

        public static explicit operator FeedEntry(SyndicationItem item)
        {
            return new FeedEntry
            {
                Title = item.Title?.Text,
                Link = item.Links?.FirstOrDefault().Uri.AbsoluteUri,
                PublishDate = item.PublishDate,
                Authors = item.Authors?.Select(e => e.Name)
            };
        }
    }
}