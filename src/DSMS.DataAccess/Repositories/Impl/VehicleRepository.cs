using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Vehicle> GetAll()
        {
            return DbSet.Include(v => v.Instructor).AsQueryable();

        }
    }
}
