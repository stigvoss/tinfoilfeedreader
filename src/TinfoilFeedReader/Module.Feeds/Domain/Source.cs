using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain
{
    public class Source : AggregateRoot<Source>
    {
        public Source()
            : base() { }

        public Source(Guid id)
            : base(id) { }

        public string Name { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
