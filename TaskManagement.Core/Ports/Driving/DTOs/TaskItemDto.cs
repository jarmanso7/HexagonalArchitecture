namespace TaskManagement.Core.Ports.Driving.DTOs
{
    public class TaskItemDto
    {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public bool IsCompleted { get; set; }
    }
}
