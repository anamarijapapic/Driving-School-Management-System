using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Appointment : BaseEntity
    {
        private Guid StudentId { get; set; }

        private Guid SlotId { get; set; }

        private Guid StatusId { get; set; }
    }
}
