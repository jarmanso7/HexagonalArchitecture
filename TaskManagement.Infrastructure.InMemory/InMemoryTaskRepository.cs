using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Infrastructure.InMemory
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private Dictionary<Guid, TaskItem> tasks = new();

        public void Save(TaskItem task)
        {
            if (tasks.ContainsKey(task.Id))
                throw new Exception("A task with the same Id already exists");

            tasks.Add(task.Id, task);
        }

        public void Update(TaskItem task)
        {
            if (! tasks.ContainsKey(task.Id))
                throw new Exception($"Task with id {task.Id} not found.");

            tasks[task.Id] = task;
        }
        public TaskItem? GetById(Guid id)
        {
            return tasks.TryGetValue(id, out TaskItem value) ? value : null;
        }
    }
}