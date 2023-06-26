#nullable disable

using DSMS.Application.Models.Feedback;
using DSMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Feedback
{
    public class EditModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;

        public EditModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UpdateFeedbackModel Input { get; set; }

        private void Load(Core.Entities.Feedback feedback)
        {
            var title = feedback.Title;
            var rating = feedback.Rating;
            var content = feedback.Content;

            Input = new UpdateFeedbackModel
            {
                Title = title,
                Rating = rating,
                Content = content
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with ID '{id}'.");
            }

            Load(feedback);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null)
            {
                return base.NotFound($"Unable to load feedback with ID '{id}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(feedback);
                return Page();
            }

            feedback.Title = Input.Title;
            feedback.Content = Input.Content;
            feedback.Rating = Input.Rating;

            await _feedbackService.UpdateAsync(feedback);

            StatusMessage = "Feedback details have been sucessfully updated.";

            return Page();
        }
    }
}
