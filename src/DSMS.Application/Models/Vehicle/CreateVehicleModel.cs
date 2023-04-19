namespace DSMS.Application.Models.Vehicle;

public class CreateVehicleModel
{
    private Guid VehicleId { get; set; }

    private Guid InstructorId { get; set; }

    private Guid CategoryId { get; set; }

    private string Description { get; set; }

    private byte[] Image { get; set; }
}

public class CreateVehicleResponseModel : BaseResponseModel { }
