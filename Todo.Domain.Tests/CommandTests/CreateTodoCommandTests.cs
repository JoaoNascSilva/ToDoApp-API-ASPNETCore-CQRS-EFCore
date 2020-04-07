using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;

namespace Todo.Domain.Tests.HandlerTest
{
    [TestClass]
    public class CreateTodoCommandTests
    {
        //* Red Green Refactor

        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Estudar", "JoãoSilva", DateTime.Now);

        public CreateTodoCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void DadoUmComandoInvalido()
        {
            Assert.AreEqual(_invalidCommand.Valid, false);

        }

        [TestMethod]
        public void DadoUmComandoValido()
        {
            Assert.AreEqual(_validCommand.Valid, true);
        }
    }
}