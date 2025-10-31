
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Adapters.Driven.EntityFramework;
using TaskManagement.Core.Ports.Driven;
using TaskManagement.Core.Ports.Driving;
using TaskManagement.Core.Ports.Services;
using TaskManagement.Core.UseCases.Commands;

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

            builder.Services.AddMediatR(typeof(CreateTaskHandler).Assembly);

            //Configure dependency injection
            builder.Services.AddScoped<ITaskRepository, EfTaskRepository>();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
            }, typeof(Program).Assembly);

            builder.Services.AddSingleton(provider =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open(); // Keep the connection open
                return connection;
            });

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
