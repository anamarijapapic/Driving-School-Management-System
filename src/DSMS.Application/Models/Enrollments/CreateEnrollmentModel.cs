﻿using DSMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Application.Models.Enrollments
{
    public class CreateEnrollmentModel : BaseResponseModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Instructor")]
        public string InstructorId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Student")]
        public string StudentId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Category Category { get; set; }
    }

    public class CreateEnrollmentResponseModel : BaseResponseModel { }
}
