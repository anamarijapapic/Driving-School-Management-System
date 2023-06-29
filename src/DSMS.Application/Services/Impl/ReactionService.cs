using AutoMapper;
using DSMS.Application.Models.Reaction;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DSMS.Application.Services.Impl
{
    public class ReactionService : IReactionService
    {
        private readonly IMapper _mapper;

        private readonly IReactionRepository _reactionRepository;

        private readonly IFeedbackRepository _feedbackRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public ReactionService(IMapper mapper,
            IReactionRepository reactionRepository,
            IFeedbackRepository feedbackRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _reactionRepository = reactionRepository;
            _feedbackRepository = feedbackRepository;
            _userManager = userManager;
        }

        public async Task<CreateReactionResponseModel> CreateAsync(CreateReactionModel createReactionModel)
        {
            var student = await _userManager.FindByIdAsync(createReactionModel.StudentId.ToString());
            var feedback = await _feedbackRepository.GetByIdAsync(createReactionModel.FeedbackId.ToString());
            var reaction = _mapper.Map<Reaction>(createReactionModel);

            reaction.Student = student;
            reaction.Feedback = feedback;

            return new CreateReactionResponseModel
            {
                Id = (await _reactionRepository.AddAsync(reaction)).Id,
            };
        }

        public async Task<IEnumerable<Reaction>> GetAllAsync()
        {
            var reactionss = await _reactionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Reaction>>(reactionss);
        }
    }
}
