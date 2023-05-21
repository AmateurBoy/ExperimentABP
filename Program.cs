using ExperimentABP.Controllers;
using ExperimentABP.Data;
using ExperimentABP.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ExperimentABP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add Configuration 
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            // Add services to the container.
            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc();  
            builder.Services.AddSession();

            //Add DI 
            builder.Services.AddTransient(_ => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<IDatabaseManager, DatabaseManager>();
            builder.Services.AddTransient<IDefaultCreator, DatabaseManager>();
            builder.Services.AddTransient<IDeterminantService, DeterminantService>();
            
            var app = builder.Build();

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