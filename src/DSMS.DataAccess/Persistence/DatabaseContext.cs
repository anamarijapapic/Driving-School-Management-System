using DSMS.Core.Common;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.Shared.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DSMS.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    private readonly IClaimService _claimService;

    public DatabaseContext(DbContextOptions options, IClaimService claimService) : base(options)
    {
        _claimService = claimService;
    }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Reaction> Reactions { get; set; }

    public DbSet<ScheduleSlot> ScheduleSlots { get; set; }

    public DbSet<Status> Statuses { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<TodoList> TodoLists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        builder
            .Entity<Category>()
            .Property(e => e.Id)
            .HasConversion<int>();

        builder
            .Entity<Category>().HasData(
                Enum.GetValues(typeof(CategoryId))
                    .Cast<CategoryId>()
                    .Select(e => new Category()
                    {
                        Id = e,
                        Name = e.ToString()
                    })
            );

        builder
            .Entity<Status>()
            .Property(e => e.Id)
            .HasConversion<int>();

        builder
            .Entity<Status>().HasData(
                Enum.GetValues(typeof(StatusId))
                    .Cast<StatusId>()
                    .Select(e => new Status()
                    {
                        Id = e,
                        Name = e.ToString()
                    })
            );

        builder.Entity<Category>()
            .HasMany(e => e.Instructors)
            .WithMany(e => e.Categories)
            .UsingEntity("CategoryInstructor");

        builder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(e => e.StudentEnrollments)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Enrollment>()
            .HasOne(e => e.Instructor)
            .WithMany(e => e.InstructorEnrollments)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
            .HasMany(e => e.StudentFeedbacks)
            .WithOne(e => e.Student)
            .IsRequired();

        builder.Entity<ApplicationUser>()
            .HasMany(e => e.InstructorFeedbacks)
            .WithOne(e => e.Instructor)
            .IsRequired();
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _claimService.GetUserId();
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _claimService.GetUserId();
                    entry.Entity.UpdatedOn = DateTime.Now;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
