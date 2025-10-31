using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Driving.DTOs;

namespace TaskManagement.Adapters.Driving.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(
            ITaskService taskService,
            IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpPost()]
        public IActionResult Create(string title)
        {
            var task = _taskService.CreateTask(title);
            return Ok(_mapper.Map<TaskItemDto>(task));
        }

        [HttpGet()]
        public IActionResult Get(string id)
        {
            var task = _taskService.GetTask(new Guid(id));
            return task == null ? NotFound() : Ok(_mapper.Map<TaskItemDto>(task));
        }

        [HttpPost()]
        public IActionResult CompleteTask(string id)
        {
            _taskService.CompleteTask(new Guid(id));

            return Ok();
        }
    }
}
