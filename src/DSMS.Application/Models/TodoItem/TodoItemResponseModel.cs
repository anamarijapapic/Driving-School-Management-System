namespace DSMS.Application.Models.TodoItem;

public class TodoItemResponseModel : BaseResponseModel
{
    public string Title { get; set; }

    public string Body { get; set; }

    public bool IsDone { get; set; }
}
