using DSMS.Application.Models.Enrollments;

namespace DSMS.Application.Services
{
    public interface IEnrollmentService
    {
        Task<CreateEnrollmentResponseModel> CreateAsync(CreateEnrollmentModel createEnrollmentModel);

        Task<IEnumerable<EnrollmentResponseModel>> GetAllAsync();

        IEnumerable<EnrollmentResponseModel> Search(IEnumerable<EnrollmentResponseModel> enrollments, string searchString);

        IEnumerable<EnrollmentResponseModel> Filter(IEnumerable<EnrollmentResponseModel> enrollments, string currentFilter);
    }
}
