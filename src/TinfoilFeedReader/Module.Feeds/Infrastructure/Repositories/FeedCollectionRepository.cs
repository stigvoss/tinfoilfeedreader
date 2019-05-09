using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Feeds.Infrastructure.Repositories
{
    public class FeedCollectionRepository : IRepository<FeedCollection>
    {
        private readonly List<FeedCollection> _collections = new List<FeedCollection>
        {
            new FeedCollection(Guid.Parse("{69040700-3B62-42CF-803E-0C26C40DB842}"))
            {
                Feeds = new List<Feed>
                {
                    new Feed {
                        Id = Guid.Parse("{4CC67215-2615-4A7A-B4A0-67A04626DBB0}"),
                        Name = "Local News",
                        Sources = new List<FeedSource>()
                    },
                    new Feed {
                        Id = Guid.Parse("{A83E49B2-13E1-4E5A-A294-B32740409C50}"),
                        Name = "Politics",
                        Sources = new List<FeedSource>()
                    },
                    new Feed
                    {
                        Id = Guid.Parse("{BFDFB55D-8887-41A5-B37C-E7FCFCC2A83E}"),
                        Name = "Technology",
                        Sources = new List<FeedSource>
                        {
                            new FeedSource
                            {
                                Id = Guid.Parse("{6F6CCD9C-F5B1-4F7F-99CF-601DA1276CAB}"),
                                Name = "Ars Technica",
                                Url = "https://feeds.feedburner.com/arstechnica/index"
                            },
                            new FeedSource
                            {
                                Id = Guid.Parse("{7F0E8887-A73A-4A97-B32E-27BF2825D08A}"),
                                Name = "The Verge",
                                Url = "https://www.theverge.com/rss/index.xml"
                            }
                        }
                    },
                    new Feed {
                        Id = Guid.Parse("{D0867B47-0DB5-446E-99E0-5F66674F4AD1}"),
                        Name = "YouTube Subscriptions",
                        Sources = new List<FeedSource>
                        {
                            new FeedSource
                            {
                                Id = Guid.Parse("{FF5A433A-2425-4711-85B9-05E4DD86A4DE}"),
                                Name = "CGP Grey",
                                Url = "https://www.youtube.com/feeds/videos.xml?channel_id=UC2C_jShtL725hvbm1arSV9w"
                            }
                        }
                    }
                }
            }
        };

        public void Add(FeedCollection item)
        {
            _collections.Add(item);
        }

        public IEnumerable<FeedCollection> All()
        {
            throw new NotSupportedException();
        }

        public FeedCollection Single(Guid id)
        {
            return _collections.FirstOrDefault(e => e.Id == id);
        }

        public void Remove(FeedCollection item)
        {
            _collections.Remove(item);
        }

        public void Update(FeedCollection item)
        {
            var index = _collections.IndexOf(item);
            _collections.RemoveAt(index);
            _collections.Insert(index, item);
        }

        public void Dispose()
        {
        }
    }
}
