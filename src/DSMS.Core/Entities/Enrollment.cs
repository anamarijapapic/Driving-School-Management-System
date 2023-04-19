using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class Enrollment : BaseEntity
    {
        public ApplicationUser Student { get; set; } = null!;

        public ApplicationUser Instructor { get; set; } = null!;

        public Category Category { get; set; }
    }
}
