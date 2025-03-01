using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;

        // Método Construtor -> usando Dependency Injection (Injeção de Depêndencia)
        public TodoHandler(ITodoRepository repository) => _repository = repository;

        public ICommandResult Handler(CreateTodoCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada.", command.Notifications);

            // Gera TodoItem
            var todoItem = new TodoItem(command.Title, command.User, command.Date);

            // Salva No Banco
            _repository.Create(todoItem);
            return new GenericCommandResult(true, "Tarefa Salva", todoItem);
        }

        public ICommandResult Handler(UpdateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada.", command.Notifications);

            // Recupera o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o Título
            todo.UpdateTitle(command.Title);

            // Salva no Banco
            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handler(MarkTodoAsDoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada.", command.Notifications);

            // Recupera o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o Estado
            todo.MarkAsDone();

            // Salva no Banco
            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handler(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada.", command.Notifications);

            // Recupera o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            // altera o Estado
            todo.MarkAsUndone();

            // Salva no Banco
            _repository.Update(todo);

            // Retorna o resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}