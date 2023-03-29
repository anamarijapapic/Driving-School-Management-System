namespace DSMS.Application.Models.Appointment;

public class CreateAppointmentModel
{
    private Guid AppointmentId { get; set; }

    private Guid StudentId  { get; set; }

    private Guid SlotId { get; set; }

    private Guid StatusId { get; set; }
}

public class CreateAppointmentResponseModel : BaseResponseModel { }
