using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<modelsContext>(options =>
                  options.UseSqlServer(builder.Configuration["ConnectionString"]));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope()) 
            {
            var services = scope.ServiceProvider;
                var context = services.GetRequiredService<modelsContext>();
                context.Database.Migrate();
            
            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                app.UseSwaggerUI();
                }


            app.UseCors(builder => builder.WithOrigins(new[] { " https://localhost:7168 ", "https://webapplication1-14.onrender.com" })
            .AllowAnyHeader()
            .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
