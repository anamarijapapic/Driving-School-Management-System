using DSMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMS.DataAccess.Repositories
{
    public interface IReactionRepository : IBaseRepository<Reaction>
    {
        Task<IEnumerable<Reaction>> GetAllAsync();

        Task<Reaction> GetByIdAsync(string id);
    }
}
