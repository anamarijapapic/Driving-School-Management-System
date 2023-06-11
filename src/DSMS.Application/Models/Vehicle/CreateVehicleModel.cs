using DSMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Application.Models.Vehicle;

public class CreateVehicleModel : BaseResponseModel
{
    [Display(Name = "Instructor")]
    public string InstructorId { get; set; }

    [Required]
    [Display(Name = "Category")]
    public Category Category { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Photo")]
    public byte[] Photo { get; set; }
}

public class CreateVehicleResponseModel : BaseResponseModel { }
