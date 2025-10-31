using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driving.DTOs;

namespace TaskManagement.Core.Ports.Driving
{
    public interface ITaskService
    {
        TaskItem CreateTask(string title);
        void CompleteTask(Guid id);
        TaskItem? GetTask(Guid id);
    }
}
