namespace DSMS.Application.Models.ScheduleSlot;

public class CreateScheduleSlotModel
{
    private Guid SlotId { get; set; }

    private Guid InstructorId { get; set; }

    private DateTime StartTime  { get; set; }

    private DateTime EndTime { get; set; }
}

public class CreateScheduleSlotResponseModel : BaseResponseModel { }
