using Module.Feeds.Domain.Base;
using Module.Feeds.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Module.Feeds.Domain
{
    public class Article : IValueObject
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public Source Source { get; set; }

        public string TimeSincePublish()
        {
            var timeSince = DateTime.Now - PublishDate.LocalDateTime;

            if (timeSince < TimeSpan.FromMinutes(1))
            {
                return $"{timeSince.Seconds}s";
            }
            else if (timeSince < TimeSpan.FromHours(1))
            {
                return $"{timeSince.Minutes}m";
            }
            else if (timeSince < TimeSpan.FromDays(1))
            {
                return $"{timeSince.Hours}h";
            }

            return $"{timeSince.Days}d";
        }

        public string Summary { get; set; }

        public string ImageUrl { get; set; }
        public string SourceName { get; internal set; }

        public static explicit operator Article(SyndicationItem item)
            => new Article
            {
                Title = item.Title?.Text,
                Url = item.Links?.FirstOrDefault().Uri.AbsoluteUri,
                PublishDate = item.PublishDate,
                Summary = item.Summary?.Text,
                ImageUrl = ContentService.ReadImageUrl(item.Content)
            };
    }
}