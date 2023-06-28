using DSMS.Application.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSMS.Core.Entities;

namespace DSMS.Application.Services
{
    public interface IReactionService
    {
        Task<CreateReactionResponseModel> CreateAsync(CreateReactionModel createReactionModel);

        Task<IEnumerable<Reaction>> GetAllAsync();
    }
}
