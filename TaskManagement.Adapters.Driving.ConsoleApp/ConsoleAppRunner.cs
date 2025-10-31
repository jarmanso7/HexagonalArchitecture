using TaskManagement.Core.Ports.Driving;

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
            Console.WriteLine($"Retrieved task {task.Id}. Title: {task.Title}");
        }
    }
}
