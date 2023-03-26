using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;

namespace DSMS.DataAccess.Repositories.Impl;

public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DatabaseContext context) : base(context) { }
}
