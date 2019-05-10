using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Base
{
    public interface ISourceRepository
        : IRepository<Source>
    {
        IQueryable<Source> All(IEnumerable<Guid> ids);
    }
}
