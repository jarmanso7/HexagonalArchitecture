using TaskManagement.Adapters.Driven.InMemory;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Services;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{

    public class ConsoleAppRunner
    {
        private readonly ITaskService _taskService;

        public ConsoleAppRunner(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to The TaskManagement Hexagonal App.");

            //Wire up adapters and ports
            ITaskRepository taskRepository = new InMemoryTaskRepository();
            ITaskService taskService = new TaskService(taskRepository);

            // Create a task
            Console.WriteLine("Creating task...");
            var task = taskService.CreateTask("My very first Task. Yay!");
            Console.WriteLine("Created!");

            // Complete it!
            Console.WriteLine("Completing task...");
            taskService.CompleteTask(task.Id);
            Console.WriteLine("Completed!");

            // Retrieve it
            Console.WriteLine("Retrieving task...");
            var retrievedTask = taskService.GetTask(task.Id);
            Console.WriteLine($"Retrieved task {task.Id}. Title: {task.Title}");
        }
    }
}
