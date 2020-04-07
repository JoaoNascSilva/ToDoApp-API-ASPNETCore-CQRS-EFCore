using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntityTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _todo = new TodoItem("Estudar", "JoaoSilva", DateTime.Now);
        [TestMethod]
        public void DadoUmNovoTodoOMesmoNaoPodeSerConcluido()
        {
            Assert.AreEqual(_todo.Done, false);
        }
    }
}