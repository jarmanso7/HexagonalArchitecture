using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.Adapters.Driven.EntityFramework;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Services;

namespace TaskManagement.Adapters.Driving.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<ITaskRepository, EfTaskRepository>();

                    services.AddScoped<ITaskService, TaskService>();

                    services.AddSingleton(new SqliteConnection("DataSource=:memory:"));
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
                var con = scope.ServiceProvider.GetRequiredService<SqliteConnection>();
                con.Open();

                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                // 3. Create the database schema
                dbContext.Database.EnsureCreated();

                var appRunner = scope.ServiceProvider.GetService<ConsoleAppRunner>();

                appRunner?.Run();
            }
        }
    }
}
