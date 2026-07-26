
using Application.PipelineBehaviors;
using Application.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

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

            // Подключение валидатора 
            builder.Services.AddValidatorsFromAssembly(typeof(CreateBranchCommandValidator).Assembly);

            builder.Services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(typeof(CreateBranchCommandValidator).Assembly);
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
