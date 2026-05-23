
using Application.Behavior.Client;
using Application.Behavior.User;
using Application.Interfaces.Repositories;
using Application.PipelineBehaviors;
using Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace WebApi
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

            builder.Services.AddPersistence(builder.Configuration);

            builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

            builder.Services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(typeof(CreateUserCommandValidator).Assembly);
                configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            using (IServiceScope scope = app.Services.CreateScope())
            {
                ApplicationContext context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate();
            }

            app.Run();
        }
    }
}
