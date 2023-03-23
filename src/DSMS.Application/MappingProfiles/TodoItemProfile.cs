using AutoMapper;
using DSMS.Application.Models.TodoItem;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<CreateTodoItemModel, TodoItem>()
            .ForMember(ti => ti.IsDone, ti => ti.MapFrom(cti => false));

        CreateMap<UpdateTodoItemModel, TodoItem>();

        CreateMap<TodoItem, TodoItemResponseModel>();
    }
}
