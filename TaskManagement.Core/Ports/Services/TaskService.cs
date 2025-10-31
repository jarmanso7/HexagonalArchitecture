using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;

namespace TaskManagement.Core.Ports.Services
{
    public class TaskService : ITaskService
    {
        // Notice it depends on the PORT, not a concrete implementation
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TaskItem CreateTask(string title)
        {
            var task = new TaskItem(title);
            _taskRepository.Save(task);

            return task;
        }

        public void CompleteTask(Guid id)
        {
            var task = _taskRepository.GetById(id);

            if (task is not null)
            {
                task.Complete();
                _taskRepository.Update(task);
            }
        }

        public TaskItem? GetTask(Guid id)
        {
            return _taskRepository.GetById(id);
        }
    }
}
