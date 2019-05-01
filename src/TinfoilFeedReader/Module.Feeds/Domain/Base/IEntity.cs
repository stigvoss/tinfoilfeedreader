using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain.Base
{
    public interface IEntity<T> : IEquatable<T>
    {
        Guid Id { get; }
    }
}
