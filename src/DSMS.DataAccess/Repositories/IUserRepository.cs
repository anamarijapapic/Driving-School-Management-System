using DSMS.Core.Entities.Identity;
using System.Linq.Expressions;

namespace DSMS.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
    }
}
