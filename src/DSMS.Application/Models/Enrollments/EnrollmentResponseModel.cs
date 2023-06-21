using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
namespace DSMS.Application.Models.Enrollments
{
    public class EnrollmentResponseModel : BaseResponseModel
    {
        public ApplicationUser Instructor { get; set; }
        public ApplicationUser Student { get; set; }
        public Category Category { get; set; }
    }
}
