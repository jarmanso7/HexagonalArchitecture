using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Driving.DTOs;

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

        public TaskItemDto CreateTask(string title)
        {
            var task = new TaskItem(title);
            _taskRepository.Save(task);

            return new TaskItemDto()
            {
                Id = task.Id,
                IsCompleted = task.IsCompleted,
                Title = task.Title
            };
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

        public TaskItemDto? GetTask(Guid id)
        {
            var task = _taskRepository.GetById(id);

            return new TaskItemDto()
            {
                Id = task.Id,
                IsCompleted = task.IsCompleted,
                Title = task.Title
            };
        }
    }
}
