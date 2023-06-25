using DSMS.DataAccess.Persistence;
using DSMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .ToListAsync();
        }
    }
}
