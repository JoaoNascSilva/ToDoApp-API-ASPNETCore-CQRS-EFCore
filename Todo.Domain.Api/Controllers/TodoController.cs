using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {
        [Route("")]
        [HttpPost]        
        public GenericCommandResult Create([FromBody]CreateTodoCommand command, [FromServices]TodoHandler handler)
        {
            command.User = "joaosilva";
            return (GenericCommandResult)handler.Handler(command);
        }
        [Route("")]
        [HttpPut]        
        public GenericCommandResult Update([FromBody]UpdateTodoCommand command, [FromServices]TodoHandler handler)
        {
            command.User = "joaosilva";
            return (GenericCommandResult)handler.Handler(command);
        }
        [Route("mark-as-done")]
        [HttpPut]        
        public GenericCommandResult MarkAsDone([FromBody]MarkTodoAsDoneCommand command, [FromServices]TodoHandler handler)
        {
            command.User = "joaosilva";
            return (GenericCommandResult)handler.Handler(command);
        }
        [Route("mark-as-undone")]
        [HttpPut]        
        public GenericCommandResult MarkAsUndone([FromBody]MarkTodoAsUndoneCommand command, [FromServices]TodoHandler handler)
        {
            command.User = "joaosilva";
            return (GenericCommandResult)handler.Handler(command);
        }


        [Route("")]
        [HttpGet]        
        public IEnumerable GetAll([FromServices]ITodoRepository repository) => repository.GetAll("joaosilva");
    
        [Route("done")]
        [HttpGet]
        public IEnumerable GetDone([FromServices]ITodoRepository repository) => repository.GetAllDone("joaosilva");
        [Route("undone")]
        [HttpGet]
        public IEnumerable GetUndone([FromServices]ITodoRepository repository) => repository.GetAllUndone("joaosilva");

        [Route("done/today")]
        [HttpGet]
        public IEnumerable GetDoneForToday([FromServices]ITodoRepository repository)
        {
            return repository.GetByPeriod("joaosilva", DateTime.Now.Date, true);
        }
        [Route("undone/today")]
        [HttpGet]
        public IEnumerable GetUndoneForToday([FromServices]ITodoRepository repository)
        {
            return repository.GetByPeriod("joaosilva", DateTime.Now.Date, false);
        }
        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable GetDoneForTomorrow([FromServices]ITodoRepository repository)
        {
            return repository.GetByPeriod("joaosilva", DateTime.Now.Date.AddDays(1), true);
        }
        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable GetUndoneForTomorrow([FromServices]ITodoRepository repository)
        {
            return repository.GetByPeriod("joaosilva", DateTime.Now.Date.AddDays(1), false);
        }
    }
}