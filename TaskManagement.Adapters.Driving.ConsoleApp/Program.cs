using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.Adapters.Driven.EntityFramework;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.UseCases.Commands;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<ITaskRepository, EfTaskRepository>();

                    services.AddMediatR(typeof(CreateTaskHandler).Assembly);

                    services.AddAutoMapper(cfg =>
                    {
                        cfg.LicenseKey = context.Configuration["AutoMapper:LicenseKey"];
                    }, typeof(Program).Assembly);

                    services.AddSingleton(provider =>
                    {
                        var connection = new SqliteConnection("DataSource=:memory:");
                        connection.Open(); // Keep the connection open
                        return connection;
                    });

                    services.AddDbContext<TaskDbContext>(
                        (serviceProvider, options) =>
                        {
                            var con = serviceProvider.GetRequiredService<SqliteConnection>();
                            options.UseSqlite(con);
                        }
                    );

                    services.AddScoped<ConsoleAppRunner>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                dbContext.Database.EnsureCreated();

                var appRunner = scope.ServiceProvider.GetService<ConsoleAppRunner>();

                await appRunner?.Run();
            }

            host.Dispose();
        }
    }
}
