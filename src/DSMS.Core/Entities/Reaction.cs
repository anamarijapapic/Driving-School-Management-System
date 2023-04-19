using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class Reaction : BaseEntity
    {
        public Feedback Feedback { get; set; } = null!;

        public ApplicationUser Student { get; set; } = null!;

        public bool IsUseful { get; set; }
    }
}
