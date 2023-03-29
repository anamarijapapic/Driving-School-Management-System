namespace DSMS.Application.Models.Status;

public class UpdateStatusModel
{
    private enum statusName
    {
        reserved,
        completed,
        canceled,
        noShow
    }
    private statusName StatusName { get; set; }
}

public class UpdateStatusResponseModel : BaseResponseModel { }
