using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain.Base
{
    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot<T>
    {
        IEnumerable<T> All();

        T Single(Guid id);

        void Remove(T item);

        void Update(T item);

        void Add(T item);
    }
}
