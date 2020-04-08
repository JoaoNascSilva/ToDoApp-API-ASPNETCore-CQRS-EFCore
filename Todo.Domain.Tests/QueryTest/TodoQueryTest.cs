using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTest
{
    [TestClass]
    public class TodoQueryTest
    {
        private List<TodoItem> _items;

        public TodoQueryTest()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("1.Estudar", "JoseValim", DateTime.Now));
            _items.Add(new TodoItem("2.Estudar", "JoaoSilva", DateTime.Now));
            _items.Add(new TodoItem("3.Estudar", "JoaoSilva", DateTime.Now));
            _items.Add(new TodoItem("4.Estudar", "JoaoSilva", DateTime.Now));
            _items.Add(new TodoItem("5.Estudar", "Gimenez.Tayer", DateTime.Now));
        }

        [TestMethod]
        public void DadaAConsultaDeveRetornarTarefasApenasDoUsuarioJoaoSilva()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("JoaoSilva"));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]        
        public void DadaAConsultaDeveRetornarTarefasConcluidasApenasDoUsuarioJoaoSilva()
        {
            _items[2].MarkAsDone();
            var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("JoaoSilva"));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]        
        public void DadaAConsultaDeveRetornarTarefasNaoConcluidasApenasDoUsuarioJoaoSilva()
        {
            _items[2].MarkAsDone();
            var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("JoaoSilva"));
            Assert.AreEqual(2, result.Count());
        }
    }
}