using Module.Feeds.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Module.Feeds.Domain
{
    public struct FeedEntry
    {
        public string Title { get; set; }

        public string SourceUrl { get; set; }

        public string SourceDomain => new Uri(SourceUrl).Host;

        public DateTimeOffset PublishDate { get; set; }

        public string SimpleTimeSincePublish()
        {
            var timeSince = DateTime.Now - PublishDate.LocalDateTime;

            if (timeSince < TimeSpan.FromMinutes(1))
            {
                return $"{Math.Round(timeSince.TotalSeconds)}s";
            }
            else if (timeSince < TimeSpan.FromHours(1))
            {
                return $"{Math.Round(timeSince.TotalMinutes)}m";
            }
            else if (timeSince < TimeSpan.FromDays(1))
            {
                return $"{Math.Round(timeSince.TotalHours)}h";
            }

            return $"{Math.Round(timeSince.TotalDays)}d";
        }

        public IEnumerable<string> Authors { get; set; }

        public string Summary { get; set; }

        public string ImageUrl { get; set; }

        public static explicit operator FeedEntry(SyndicationItem item)
            => new FeedEntry
            {
                Title = item.Title?.Text,
                SourceUrl = item.Links?.FirstOrDefault().Uri.AbsoluteUri,
                PublishDate = item.PublishDate,
                Authors = item.Authors?.Select(e => e.Name),
                Summary = item.Summary?.Text,
                ImageUrl = ContentService.ReadImage(item.Content)
            };
    }
}