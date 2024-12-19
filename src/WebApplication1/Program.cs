using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApplication1.Authorization;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;
using static System.Net.WebRequestMethods;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<modelsContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString"]));






            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Backend API",
                    Description = "Backend API ASP .NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Internet Shop",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Internet Shop",
                        Url = new Uri("https://example.com/license")
                    },
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IMapper, Mapper>();
            // builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddMapster();

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

            app.UseCors(builder => builder.WithOrigins(new[] { "https://localhost:7098", "https://yanko-web-api-with-db.onrender.com" })

            .AllowAnyHeader()
            .AllowAnyMethod());


            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
    }

