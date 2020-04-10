using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{
    [ApiController]
    [Authorize]
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
        public IEnumerable GetAll([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAll(user);
        }

        [Route("done")]
        [HttpGet]
        public IEnumerable GetDone([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllDone(user);
        }
        [Route("undone")]
        [HttpGet]
        public IEnumerable GetUndone([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllUndone(user);
        }


        [Route("done/today")]
        [HttpGet]
        public IEnumerable GetDoneForToday([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date, true);
        }

        [Route("undone/today")]
        [HttpGet]
        public IEnumerable GetUndoneForToday([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date, false);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable GetDoneForTomorrow([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), true);
        }
        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable GetUndoneForTomorrow([FromServices]ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), false);
        }
    }
}