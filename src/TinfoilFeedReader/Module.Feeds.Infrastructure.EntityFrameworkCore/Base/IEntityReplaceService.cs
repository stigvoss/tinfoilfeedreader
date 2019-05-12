using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Base
{
    public interface IEntityReplaceService
    {
        void Replace<T>(T current, T replacement)
            where T : IEntity<T>;
    }
}
