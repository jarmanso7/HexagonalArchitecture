using MediatR;
using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Core.UseCases.Commands
{
    public record CreateTaskCommand(string Title) : IRequest<TaskItem>;

    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;
        public CreateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<TaskItem> Handle(CreateTaskCommand request, CancellationToken cancel)
        {
            var task = new TaskItem(request.Title);
            _taskRepository.Save(task);

            return Task.FromResult(task);
        }
    }
}
