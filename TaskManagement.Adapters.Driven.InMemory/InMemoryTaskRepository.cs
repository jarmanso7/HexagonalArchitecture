using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Adapters.Driven.InMemory
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly Dictionary<Guid, TaskItem> _tasks = new();

        public void Save(TaskItem task)
        {
            if (_tasks.ContainsKey(task.Id))
                throw new Exception("A task with the same Id already exists");

            _tasks.Add(task.Id, task);
        }

        public void Update(TaskItem task)
        {
            if (! _tasks.ContainsKey(task.Id))
                throw new Exception($"Task with id {task.Id} not found.");

            _tasks[task.Id] = task;
        }
        public TaskItem? GetById(Guid id)
        {
            return _tasks.TryGetValue(id, out TaskItem? value) ? value : null;
        }
    }
}