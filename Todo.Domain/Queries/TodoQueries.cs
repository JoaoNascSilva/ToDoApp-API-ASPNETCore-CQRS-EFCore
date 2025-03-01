using System;
using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class TodoQueries
    {
        // Obter Todos os Users
        public static Expression<Func<TodoItem, bool>> GetAll(string user) => x => x.User == user;
        public static Expression<Func<TodoItem, bool>> GetAllDone(string user) => x => x.User == user && x.Done;
        public static Expression<Func<TodoItem, bool>> GetAllUndone(string user) => x => x.User == user && x.Done == false;
        public static Expression<Func<TodoItem, bool>> GetByPeriod(string user, DateTime date, bool done) => x => x.User == user && x.Done == done && x.Date.Date == date.Date;
    }
}