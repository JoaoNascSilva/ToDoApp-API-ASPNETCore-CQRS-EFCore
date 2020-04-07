using System;
using Todo.Domain.Entities;
using Todo.Domain.Repositories.cs;

namespace Todo.Domain.Tests.Repositories
{
    public class FakeTodoRepository : ITodoRepository
    {
        public void Create(TodoItem todo) { }
        public void Update(TodoItem todo) { }

        public TodoItem GetById(Guid id, string user)
        {
            return new TodoItem("Estudar", "João Silva", DateTime.Now);
        }
    }
}