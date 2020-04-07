using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("","", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Estudar","João Silva", DateTime.Now);
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());


        [TestMethod]
        public void DadoUmComandoInvalido_DeveInterromperAExecucao()
        {
            var result = (GenericCommandResult)_handler.Handler(_invalidCommand);

            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void DadoUmComandoValido_DeveCriarATarefa()
        {
            var result = (GenericCommandResult)_handler.Handler(_validCommand);
            Assert.AreEqual(result.Success, true);
        }
    }
}