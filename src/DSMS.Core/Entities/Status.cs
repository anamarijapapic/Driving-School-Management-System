using DSMS.Core.Common;
using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Status : BaseEntity, IAuditedEntity
    {
        private Guid StatusId { get; set; }

        private StatusName Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
