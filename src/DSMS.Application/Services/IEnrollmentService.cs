using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Services
{
    public interface IEnrollmentService
    {
        Task<CreateEnrollmentResponseModel> CreateAsync(CreateEnrollmentModel createEnrollmentModel);

        Task<IEnumerable<EnrollmentResponseModel>> GetAllAsync();

        Task<IEnumerable<EnrollmentResponseModel>> GetByStudentAsync(ApplicationUser student);

        IEnumerable<EnrollmentResponseModel> Search(IEnumerable<EnrollmentResponseModel> enrollments, string searchString);

        IEnumerable<EnrollmentResponseModel> Filter(IEnumerable<EnrollmentResponseModel> enrollments, string currentFilter);
    }
}
