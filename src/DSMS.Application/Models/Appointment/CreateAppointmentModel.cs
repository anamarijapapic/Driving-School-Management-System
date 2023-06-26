using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Application.Models.Appointment
{
    public class CreateAppointmentModel
    {
        [Display(Name = "Instructor")]
        [DataType(DataType.Text)]
        public ApplicationUser Instructor { get; set; }

        [Display(Name = "Student")]
        [DataType(DataType.Text)]
        public ApplicationUser Student { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start")]
        public TimeOnly Start { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End")]
        public TimeOnly End { get; set; }

        [Display(Name = "Status")]
        public AppointmentStatus Status { get; set; }
    }
}
