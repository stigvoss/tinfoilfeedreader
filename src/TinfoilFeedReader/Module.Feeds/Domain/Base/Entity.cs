using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain.Base
{
    public abstract class Entity<T> : IEntity<T>
        where T : IEntity<T>
    {
        public Entity()
        { }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public bool Equals(T other)
        {
            return Id.Equals(other?.Id);
        }
    }
}
