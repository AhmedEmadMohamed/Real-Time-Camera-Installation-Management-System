
using Microsoft.EntityFrameworkCore;
using Real_Time_Camera_Installation_Management_System.Data;
using Real_Time_Camera_Installation_Management_System.Hubs;
using Real_Time_Camera_Installation_Management_System.Repos.Implmentations;
using Real_Time_Camera_Installation_Management_System.Repos.Interfaces;

namespace Real_Time_Camera_Installation_Management_System
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

            builder.Services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectConnectionString"));
            });

            builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
            builder.Services.AddScoped<IJobRepo, JobRepo>();

            builder.Services.AddSignalR();
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowFrontend", policy => {
                    policy.WithOrigins("http://127.0.0.1:5500")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); 
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapHub<JobHub>("/jobHub");


            app.MapControllers();

            app.Run();
        }
    }
}
