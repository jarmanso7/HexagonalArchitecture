using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Domain;

namespace TaskManagement.Adapters.Driven.EntityFramework
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var taskItemBuilder = modelBuilder.Entity<TaskItem>();

            taskItemBuilder.HasKey(t => t.Id);

            taskItemBuilder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(30);

            taskItemBuilder.Property(t => t.IsCompleted);
        }
    }
}
