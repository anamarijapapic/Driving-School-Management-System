
namespace DSMS.Application.Models.Enrollments
{
    public class UpdateEnrollmentModel
    {
        private Guid InstructorId { get; set; }
        private Guid StudentId { get; set; }
        private Guid CategoryId { get; set; }
    }
    public class UpdateEnrollmentResponseModel: BaseResponseModel { }
}
