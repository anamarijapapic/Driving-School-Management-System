using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        private Guid InstructorId { get; set; }

        private Guid CategoryId { get; set; }

        private string Description { get; set; }

        private byte[] Image { get; set; }
    }
}
