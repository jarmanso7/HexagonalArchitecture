using TaskManagement.Core.Domain;

namespace TaskManagement.Core.Ports.Driven
{
    public interface ITaskRepository
    {
        void Save(TaskItem task);
        void Update(TaskItem task);
        TaskItem? GetById(Guid id);
    }
}
