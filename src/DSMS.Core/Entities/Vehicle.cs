using DSMS.Core.Common;

namespace DSMS.Core.Entities
{
    public class Vehicle : BaseEntity, IAuditedEntity
    {
        private Guid VehicleId { get; set; }

        private Guid InstructorId { get; set; }

        private Guid CategoryId { get; set; }

        private string Description { get; set; }

        private byte[] Image { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
