using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;

namespace Module.Feeds.Domain
{
    public class Feed : Entity<Feed>
    {
        public Feed()
            : base()
        { }

        public Feed(Guid id)
            : base(id)
        { }

        public int SequenceIndex { get; set; }

        public string Name { get; set; }

        public virtual ICollection<FeedSource> FeedSources { get; set; } = new List<FeedSource>();
    }
}
