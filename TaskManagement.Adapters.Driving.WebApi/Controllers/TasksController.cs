using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Ports.Driving.DTOs;
using TaskManagement.Core.UseCases.Commands;
using TaskManagement.Core.UseCases.Queries;

namespace TaskManagement.Adapters.Driving.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TasksController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> Create(string title)
        {
            var command = new CreateTaskCommand(title);
            var task = await _mediator.Send(command); //Returns the TaskItem entity
            return Ok(_mapper.Map<TaskItemDto>(task));
        }

        [HttpGet()]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetTaskQuery(new Guid(id));
            var task = await _mediator.Send(query);
            return task == null ? NotFound() : Ok(_mapper.Map<TaskItemDto>(task));
        }

        [HttpPost()]
        public async Task<IActionResult> CompleteTask(string id)
        {
            var command = new CompleteTaskCommand(new Guid(id));
            await _mediator.Send(command);
            return Ok();
        }
    }
}
