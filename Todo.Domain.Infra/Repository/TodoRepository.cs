using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;

        // Construtor
        public TodoRepository(DataContext context)
        {
            this._context = context;
        }

        #region CRUD
        public void Create(TodoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void Update(TodoItem todo)
        {
            //* O Entity olha campo a campo se tem alterção para que assim possa realizar o update nas alterações
            _context.Entry(todo).State = EntityState.Modified;
            _context.SaveChanges();
        }
        #endregion

        #region Queries
        public TodoItem GetById(Guid id, string user)
        {
            return _context.Todos
                    .FirstOrDefault(x => x.Id == id && x.User == user);
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _context.Todos
                    .AsNoTracking()
                    .Where(TodoQueries.GetAll(user))
                    .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
            return _context.Todos
                    .AsNoTracking()
                    .Where(TodoQueries.GetAllDone(user))
                    .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllUndone(string user)
        {
            return _context.Todos
                    .AsNoTracking()
                    .Where(TodoQueries.GetAllUndone(user))
                    .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _context.Todos
                    .AsNoTracking()
                    .Where(TodoQueries.GetByPeriod(user, date, done))
                    .OrderBy(x => x.Date);
        }
        #endregion
    }
}