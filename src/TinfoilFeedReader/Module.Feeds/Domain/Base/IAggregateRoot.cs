using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain.Base
{
    public interface IAggregateRoot<T> 
        : IEntity<T>
    {
    }
}
