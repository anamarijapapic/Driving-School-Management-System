using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
    }
}
