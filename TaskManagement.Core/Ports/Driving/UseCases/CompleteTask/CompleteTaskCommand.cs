using MediatR;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Core.Ports.Driving.UseCases.CompleteTask
{
    public record CompleteTaskCommand(Guid Id) : IRequest;

    public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        public CompleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<Unit> Handle(CompleteTaskCommand request, CancellationToken cancel)
        {
            var task = _taskRepository.GetById(request.Id);

            if (task is not null)
            {
                task.Complete();
                _taskRepository.Update(task);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
