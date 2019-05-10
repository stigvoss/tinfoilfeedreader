﻿using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Module.Feeds.Domain.Base
{
    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot<T>
    {
        Task<IEnumerable<T>> All();

        Task<T> Single(Guid id);

        Task Remove(T item);

        Task Update(T item);

        Task Add(T item);
    }
}
