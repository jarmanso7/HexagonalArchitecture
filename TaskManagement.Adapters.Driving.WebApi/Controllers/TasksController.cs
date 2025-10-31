using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Ports.Driving;

namespace TaskManagement.Adapters.Driving.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost()]
        public IActionResult Create(string title)
        {
            var task = _taskService.CreateTask(title);
            return Ok(task);
        }

        [HttpGet()]
        public IActionResult Get(string id)
        {
            var task = _taskService.GetTask(new Guid(id));
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost()]
        public IActionResult CompleteTask(string id)
        {
            _taskService.CompleteTask(new Guid(id));

            return Ok();
        }
    }
}
