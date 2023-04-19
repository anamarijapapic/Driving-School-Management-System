using DSMS.Core.Common;
using DSMS.Core.Entities.Identity;

namespace DSMS.Core.Entities
{
    public class Vehicle : BaseEntity
    {
#nullable enable
        public ApplicationUser? Instructor { get; set; }
#nullable disable

        public Category Category { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }
    }
}
