﻿#nullable disable

using DSMS.Application.Models.Feedback;
using DSMS.Application.Models.Reaction;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFeedbackService _feedbackService;
        private readonly IReactionService _reactionService;

        public DetailsModel(IUserService userService,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IFeedbackService feedbackService,
            IReactionService reactionService,
            IReactionRepository reactionRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            _feedbackService = feedbackService;
            _userManager = userManager;
            _feedbackService = feedbackService;
            _reactionService = reactionService;
            _reactionRepository = reactionRepository;
        }

        [BindProperty]
        public CreateFeedbackModel Input { get; set; }

        [BindProperty]
        public CreateReactionModel ReactionInput { get; set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Core.Entities.Feedback Feedback { get; private set; }

        public string UserRole { get; private set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            ApplicationUser = user;

            var roles = await _userManager.GetRolesAsync(user);

            UserRole = roles.First();
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return base.BadRequest($"Unable to load user with ID '{id}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return base.BadRequest($"Unable to load user with ID '{id}'.");
            }

            await LoadAsync(user);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var student = await _userManager.GetUserAsync(User);

            Input.InstructorId = ApplicationUser.Id;
            Input.StudentId = student.Id;
            Input.CreatedOn = DateTime.Now;
            Input.IsAnonymous = false;

            try
            {
                await _feedbackService.CreateAsync(Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLike(string Id, string feedbackId, int likeCount, int dislikeCount)
        {
            var student = await _userManager.GetUserAsync(User);

            ReactionInput.IsUseful = true;
            ReactionInput.StudentId = student.Id;
            ReactionInput.FeedbackId = feedbackId;

            var user = await _userRepository.GetByIdAsync(Id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{Id}'.");
            }

            await LoadAsync(user);

            if (likeCount < 1)
            {
                if (dislikeCount < 1)
                {
                    try
                    {
                        await _reactionService.CreateAsync(ReactionInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    var feedback = _feedbackService.GetByIdAsync(feedbackId);
                    var result = await _reactionRepository.GetAllAsync();
                    var reaction = result.Where(r => r.Student.UserName == User.Identity?.Name && r.Feedback.Id.ToString() == feedbackId).First();
                    if (reaction == null)
                    {
                        return base.NotFound("Unable to load reaction");
                    }

                    await _reactionRepository.DeleteAsync(reaction);

                    try
                    {
                        await _reactionService.CreateAsync(ReactionInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                var feedback = _feedbackService.GetByIdAsync(feedbackId);
                var result = await _reactionRepository.GetAllAsync();
                var reaction = result.Where(r => r.Student.UserName == User.Identity?.Name && r.Feedback.Id.ToString() == feedbackId).First();
                if (reaction == null)
                {
                    return base.NotFound("Unable to load reaction");
                }

                await _reactionRepository.DeleteAsync(reaction);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDislike(string Id, string feedbackId, int likeCount, int dislikeCount)
        {
            var student = await _userManager.GetUserAsync(User);

            ReactionInput.IsUseful = false;
            ReactionInput.StudentId = student.Id;
            ReactionInput.FeedbackId = feedbackId;
            var user = await _userRepository.GetByIdAsync(Id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{Id}'.");
            }

            await LoadAsync(user);

            if (dislikeCount < 1)
            {
                if (likeCount < 1)
                {
                    try
                    {
                        await _reactionService.CreateAsync(ReactionInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    var feedback = _feedbackService.GetByIdAsync(feedbackId);
                    var result = await _reactionRepository.GetAllAsync();
                    var reaction = result.Where(r => r.Student.UserName == User.Identity?.Name && r.Feedback.Id.ToString() == feedbackId).First();
                    if (reaction == null)
                    {
                        return base.NotFound("Unable to load reaction");
                    }

                    await _reactionRepository.DeleteAsync(reaction);

                    try
                    {
                        await _reactionService.CreateAsync(ReactionInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                var feedback = _feedbackService.GetByIdAsync(feedbackId);
                var result = await _reactionRepository.GetAllAsync();
                var reaction = result.Where(r => r.Student.UserName == User.Identity?.Name && r.Feedback.Id.ToString() == feedbackId).First();
                if (reaction == null)
                {
                    return base.NotFound("Unable to load reaction");
                }

                await _reactionRepository.DeleteAsync(reaction);
            }

            return Page();
        }
    }
}
