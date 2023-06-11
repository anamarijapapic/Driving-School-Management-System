using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;

namespace DSMS.Application.Models.Vehicle
{
    public class VehicleResponseModel : BaseResponseModel
    {
        public ApplicationUser Instructor { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public byte[] Photo { get; set; }
    }
}
