namespace DSMS.Application.Models.Status;

public class CreateStatusModel
{
    private Guid StatusId { get; set; }

    private enum statusName
    {
        reserved,
        completed,
        canceled,
        noShow
    }
    private statusName StatusName { get; set; }
}

public class CreateStatusResponseModel : BaseResponseModel { }
