using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.Services
{
    public interface IEnrollmentService
    {
        Task<CreateEnrollmentResponseModel> CreateAsync(CreateEnrollmentModel createEnrollmentModel);

        Task<IEnumerable<EnrollmentResponseModel>> GetAllAsync();

        Task<IEnumerable<EnrollmentResponseModel>> GetByStudentAsync(ApplicationUser student);

        Task<Enrollment> GetByIdAsync(string id);

        Task<Enrollment> UpdateAsync(Enrollment enrollment);

        Task<Enrollment> DeleteAsync(Enrollment enrollment);

        IEnumerable<EnrollmentResponseModel> Search(IEnumerable<EnrollmentResponseModel> enrollments, string searchString);

        IEnumerable<EnrollmentResponseModel> Filter(IEnumerable<EnrollmentResponseModel> enrollments, string currentFilter);
    }
}
