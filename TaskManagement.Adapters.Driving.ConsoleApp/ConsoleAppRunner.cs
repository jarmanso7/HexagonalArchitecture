using AutoMapper;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Driving.DTOs;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{

    public class ConsoleAppRunner
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public ConsoleAppRunner(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to The TaskManagement Hexagonal App.");

            // Create a task
            Console.WriteLine("Creating task...");
            var task = _taskService.CreateTask("My very first Task. Yay!");
            Console.WriteLine("Created!");

            // Complete it!
            Console.WriteLine("Completing task...");
            _taskService.CompleteTask(task.Id);
            Console.WriteLine("Completed!");

            // Retrieve it
            Console.WriteLine("Retrieving task...");
            var retrievedTask = _taskService.GetTask(task.Id);

            var dtoTask = _mapper.Map<TaskItemDto>(retrievedTask);
            Console.WriteLine($"Retrieved task {dtoTask.Id}. Title: {dtoTask.Title}");
        }
    }
}
