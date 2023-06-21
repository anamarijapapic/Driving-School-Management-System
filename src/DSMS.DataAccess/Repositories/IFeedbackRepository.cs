using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IFeedbackRepository : IBaseRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllAsync();

    }
}
