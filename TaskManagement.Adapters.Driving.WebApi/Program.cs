
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Adapters.Driven.EntityFramework;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Services;

namespace TaskManagement.Adapters.Driving.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configure dependency injection
            builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();

            // 1. Create a single, open connection.
            //    It must be a singleton to keep the database alive.
            const string connectionString = "DataSource=:memory:";
            var connection = new SqliteConnection(connectionString);
            connection.Open(); // <-- This keeps the connection open

            builder.Services.AddSingleton(connection);

            builder.Services.AddDbContext<TaskDbContext>(
                (serviceProvider, options) =>
                {
                    var con = serviceProvider.GetRequiredService<SqliteConnection>();
                    options.UseSqlite(con);
                }
            );


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
