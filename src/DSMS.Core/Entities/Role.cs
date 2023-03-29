using DSMS.Core.Common;
using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Role : BaseEntity
    {
        private Guid RoleId { get; set; }

        private RoleName Name { get; set; }
    }
}
