using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Category
    {
        public CategoryId Id { get; set; }

        public string Name { get; set; }

        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();

        public List<ApplicationUser> Instructors { get; } = new();

        public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
    }
}
