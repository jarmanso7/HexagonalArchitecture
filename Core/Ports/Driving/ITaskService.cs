using TaskManagement.Core.Domain;

namespace TaskManagement.Core.Ports.Driving
{
    public interface ITaskService
    {
        TaskItem CreateTask(string title);
        void CompleteTask(Guid id);
        TaskItem? GetTaks(Guid id);
    }
}
