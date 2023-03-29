using DSMS.Core.Common;
using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Status : BaseEntity
    {
        private Guid StatusId { get; set; }

        private StatusName Name { get; set; }
    }
}
