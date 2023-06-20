﻿using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await DbSet
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .ToListAsync();
        }
    }
}
