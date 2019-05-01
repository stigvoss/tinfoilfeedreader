using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain.Base
{
    public abstract class AggregateRoot<T> : IAggregateRoot<T>
        where T : IAggregateRoot<T>
    {
        public AggregateRoot()
        { }

        public AggregateRoot(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public virtual bool Equals(T other)
        {
            return Id.Equals(other?.Id);
        }
    }
}
