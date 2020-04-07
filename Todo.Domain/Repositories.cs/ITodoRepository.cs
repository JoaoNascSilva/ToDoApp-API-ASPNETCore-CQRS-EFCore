using System;
using Todo.Domain.Entities;

namespace Todo.Domain.Repositories.cs
{
    public interface ITodoRepository
    {
         void Create(TodoItem todo);
         void Update(TodoItem todo);
         TodoItem GetById(Guid id, string user);
    }
}