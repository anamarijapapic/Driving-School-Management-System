namespace DSMS.Application.Models.Appointment;

public class UpdateAppointmentModel
{
    private Guid AppointmentId { get; set; }

    private Guid StudentId { get; set; }

    private Guid SlotId { get; set; }

    private Guid StatusId { get; set; }
}

public class UpdateAppointmentResponseModel : BaseResponseModel { }
