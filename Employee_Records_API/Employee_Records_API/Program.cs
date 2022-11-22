
using Employee_Records_API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Stored;

namespace Employee_Records_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IRepository, Repository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddSwaggerGen(e => e.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Employee Records API",
                Description = "An ASP.NET 7 Web API for managing Employees",
                Contact = new OpenApiContact
                {
                    Name = "Mostafa Mahmoud",
                    Url = new Uri("https://www.linkedin.com/in/mmr16"),
                    Email = "mmnear16@gmail.com"
                }

            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(e => e.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}