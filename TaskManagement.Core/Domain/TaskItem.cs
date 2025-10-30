namespace TaskManagement.Core.Domain
{
    public class TaskItem
    {
        public Guid Id { get; }
        public string Title { get; private set; }
        public bool IsCompleted { get; private set; }

        public TaskItem(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            IsCompleted = true;
        }

        internal void Complete()
        {
            IsCompleted = true;
        }
    }
}