using MediatR;
using TaskManagement.Core.Domain;
using TaskManagement.Core.Ports.Driven;

namespace TaskManagement.Core.UseCases.Queries
{
    public record GetTaskQuery(Guid Id) : IRequest<TaskItem?>;

    public class GetTaskHandler: IRequestHandler<GetTaskQuery, TaskItem?>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<TaskItem?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_taskRepository.GetById(request.Id));
        }
    }
}
