
using Microshop.DatabaseFirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Microshop.DatabaseFirstApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Registrera OrderDbContext med SQL Server
            var connectionString = builder.Configuration.GetConnectionString("OrderDbConnection");
            builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(connectionString));

            // IgnoreCycles: Customer -> Orders -> Customer skulle annars ge en oändlig loop i JSON
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
