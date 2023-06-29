using DSMS.Core.Entities.Identity;

namespace DSMS.DataAccess.Repositories
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAll();

        Task<ApplicationUser> GetByIdAsync(string id);
    }
}
