using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class CreateTodoCommand : Notifiable, ICommand
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }        

        public CreateTodoCommand() {}
        public CreateTodoCommand(string title, string user, DateTime date)
        {
            this.Title = title;
            this.User = user;
            this.Date = date;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Title, 3, "Tilte", "Por favor, descreva melhor esta tarefa.")
                    .HasMinLen(User, 6, "User", "Usuário Inválido.")
            );
        }
    }
}