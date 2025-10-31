using AutoMapper;
using MediatR;
using TaskManagement.Core.Ports.Driving.DTOs;
using TaskManagement.Core.UseCases.Commands;
using TaskManagement.Core.UseCases.Queries;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{

    public class ConsoleAppRunner
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ConsoleAppRunner(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Run()
        {
            Console.WriteLine("Welcome to The TaskManagement Hexagonal App.");

            // Create a task
            Console.WriteLine("Creating task...");
            var createCommand = new CreateTaskCommand("My very first Task. Yay!");
            var task = await _mediator.Send(createCommand);
            Console.WriteLine("Created!");

            // Complete it!
            Console.WriteLine("Completing task...");
            var completeCommand = new CompleteTaskCommand(task.Id);
            _ = await _mediator.Send(completeCommand);
            Console.WriteLine("Completed!");

            // Retrieve it
            Console.WriteLine("Retrieving task...");
            var getQuery = new GetTaskQuery(task.Id);
            var retrievedTask = await _mediator.Send(getQuery);

            var dtoTask = _mapper.Map<TaskItemDto>(retrievedTask);
            Console.WriteLine($"Retrieved task {dtoTask.Id}. Title: {dtoTask.Title}");
        }
    }
}
