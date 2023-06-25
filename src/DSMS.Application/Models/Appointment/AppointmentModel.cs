using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DSMS.Application.Models.Appointment
{
    public class AppointmentModel
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

    public class AppointmentResponseModel : BaseResponseModel { }
}
