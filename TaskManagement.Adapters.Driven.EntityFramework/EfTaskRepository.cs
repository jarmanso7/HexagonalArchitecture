using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Adapters.Driven.EntityFramework
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public EfTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public TaskItem? GetById(Guid id) => _context.Tasks.Find(id);
        public void Save(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }
        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
