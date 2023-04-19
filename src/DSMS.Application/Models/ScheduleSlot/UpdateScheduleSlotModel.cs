namespace DSMS.Application.Models.ScheduleSlot;

public class UpdateScheduleSlotModel
{
    private Guid SlotId { get; set; }

    private Guid InstructorId { get; set; }

    private DateTime StartTime { get; set; }

    private DateTime EndTime { get; set; }
}

public class UpdateScheduleSlotResponseModel : BaseResponseModel { }
