using DSMS.Application.Models.User;
using DSMS.Application.Models.Feedback;
using DSMS.Core.Entities;

namespace DSMS.Application.Services
{
    public interface IFeedbackService
    {
        Task<CreateFeedbackResponseModel> CreateAsync(CreateFeedbackModel createFeedbackModel);

        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetByInstructorAsync(string Id);
    }
}
