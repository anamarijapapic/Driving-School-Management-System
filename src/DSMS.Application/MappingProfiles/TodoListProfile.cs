using AutoMapper;
using DSMS.Application.Models.TodoList;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<CreateTodoListModel, TodoList>();

        CreateMap<TodoList, TodoListResponseModel>();
    }
}
