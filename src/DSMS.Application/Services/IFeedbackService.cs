using DSMS.Application.Models.Feedback;
using DSMS.Core.Entities;

namespace DSMS.Application.Services
{
    public interface IFeedbackService
    {
        Task<CreateFeedbackResponseModel> CreateAsync(CreateFeedbackModel createFeedbackModel);

        Task<IEnumerable<Feedback>> GetAllAsync();

        Task<IEnumerable<Feedback>> GetByInstructorAsync(string id);

        Task<Feedback> GetByIdAsync(string id);

        Task<Feedback> UpdateAsync(Feedback feedback);

        Task<Feedback> DeleteAsync(Feedback feedback);
    }
}
