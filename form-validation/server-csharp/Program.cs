using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace ServerCSharp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var app = BuildApp(args);

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }

        static WebApplication BuildApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder
                .Services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        "v0",
                        new OpenApiInfo
                        {
                            Title = "Temper Validation Demo Server",
                            Description = "Demo Temper-Built Validation Logic in C# Server",
                            Version = "v0"
                        }
                    );
                });
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v0/swagger.json", "Temper Validation Demo");
            });
            return app;
        }
    }
}
