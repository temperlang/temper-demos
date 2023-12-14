using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace ServerCSharp
{
    record RawForm(double MinValue, double MaxValue);

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
            // Add cors.
            builder
                .Services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                            builder
                                .WithOrigins("http://localhost:9000")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                    );
                });
            // Add Swagger service.
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
            // Use cors.
            app.UseCors();
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
