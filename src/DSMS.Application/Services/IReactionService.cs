using DSMS.Application.Models.Reaction;
using DSMS.Core.Entities;

namespace DSMS.Application.Services
{
    public interface IReactionService
    {
        Task<CreateReactionResponseModel> CreateAsync(CreateReactionModel createReactionModel);

        Task<IEnumerable<Reaction>> GetAllAsync();
    }
}
