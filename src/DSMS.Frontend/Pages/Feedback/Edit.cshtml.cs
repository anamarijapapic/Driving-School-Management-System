#nullable disable



using DSMS.Application.Models.Feedback;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class EditModel : PageModel
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IFeedbackRepository feedbackRepository, IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
            _userManager = userManager;

        }

        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public CreateFeedbackModel Input { get; set; }
        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.FirstOrDefault();
            if (feedback != null)
            {
                return base.NotFound($"Unable to load feedback with ID '{Id}'.");
            }
            await Load(feedback);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var result = await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == Id);
            var feedback = result.FirstOrDefault();
            if (feedback != null)
            {
                return base.NotFound($"Unable to load feedback with Id '{Id}'.");
            }

            if(!ModelState.IsValid)
            {
                await Load(feedback);
                return Page();
;           }

            feedback.Title = Input.Title;
            feedback.Content = Input.Content;
            feedback.Rating= Input.Rating;

            await _feedbackRepository.UpdateAsync(feedback);

            StatusMessage = "Feedback details have been sucessfully updated.";

            return Page();
        }

        private Task Load(Core.Entities.Feedback feedback)
        {
            var title = feedback.Title;
            var rating = feedback.Rating;
            var content = feedback.Content;

            Input = new CreateFeedbackModel
            {
                Title = title,
                Rating = rating,
                Content = content
            };
            return null;
        }
    }
}
