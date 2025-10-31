using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Services;
using TaskManagement.Adapters.Driven.InMemory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<ITaskRepository, InMemoryTaskRepository>();

                    services.AddScoped<ITaskService, TaskService>();

                    services.AddScoped<ConsoleAppRunner>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            
            var appRunner = scope.ServiceProvider.GetService<ConsoleAppRunner>();

            appRunner?.Run();
        }
    }
}
