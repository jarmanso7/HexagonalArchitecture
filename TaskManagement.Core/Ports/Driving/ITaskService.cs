using TaskManagement.Core.Ports.Driving.DTOs;

namespace TaskManagement.Core.Ports.Driving
{
    public interface ITaskService
    {
        TaskItemDto CreateTask(string title);
        void CompleteTask(Guid id);
        TaskItemDto? GetTask(Guid id);
    }
}
