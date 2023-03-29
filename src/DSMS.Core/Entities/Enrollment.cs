using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Enrollment : BaseEntity
    {
        private Guid StudentId { get; set; }

        private Guid CategoryId { get; set; }

        private Guid InstructorId { get; set; }
    }
}
