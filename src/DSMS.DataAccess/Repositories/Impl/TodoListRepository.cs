using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;

namespace DSMS.DataAccess.Repositories.Impl;

public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    public TodoListRepository(DatabaseContext context) : base(context) { }
}
