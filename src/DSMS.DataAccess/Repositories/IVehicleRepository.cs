using DSMS.Core.Entities;

namespace DSMS.DataAccess.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        IQueryable<Vehicle> GetAll();
    }
}
