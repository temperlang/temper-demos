using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System;

namespace ServerCSharp
{
    class RawForm
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }

    record Result(string Message);

    static class Program
    {
        static void Main(string[] args)
        {
            var app = BuildApp(args);

            app.MapPost(
                "/",
                (RawForm form) =>
                {
                    Console.WriteLine(form);
                    return new Result(Message: "Form processed in C#");
                }
            );

            app.Run();
        }

        static WebApplication BuildApp(string[] args)
        {
            var version = "v0";
            var builder = WebApplication.CreateBuilder(args);
            // Configure Swagger service.
            builder.Services.AddEndpointsApiExplorer();
            builder
                .Services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        version,
                        new OpenApiInfo
                        {
                            Title = "Temper Validation Demo Server",
                            Description = "Demo Temper-Built Validation Logic in C# Server",
                            Version = version
                        }
                    );
                });
            // Build app.
            var app = builder.Build();
            // Use Swagger.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", "Temper Validation Demo");
            });
            // Done.
            return app;
        }
    }
}
