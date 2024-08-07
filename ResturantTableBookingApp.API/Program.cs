
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ResturantTableBookingApp.API;
using ResturantTableBookingApp.Data;
using ResturantTableBookingApp.Service;
using Serilog;
using System.Net;

namespace ResturantTableBookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Configure Serilog with the settings
                Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .CreateBootstrapLogger();

                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddApplicationInsightsTelemetry();

                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .WriteTo.ApplicationInsights(
                  services.GetRequiredService<TelemetryConfiguration>(),
                  TelemetryConverter.Events));

                Log.Information("Starting the application...");

                // Add services to the container.

                // DI container configure one instance per http request
                builder.Services.AddScoped<IResturantRepository, ResturantRepository>();
                builder.Services.AddScoped<IResturantService, ResturantService>();

                var configuration = builder.Configuration;
                builder.Services.AddDbContext<ResturantTableBookingDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DbContext") ?? "")
                    //.EnableSensitiveDataLogging() // should not be used in production purpose use only for development
                    );

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                app.UseExceptionHandler(erroApp =>
                {
                    erroApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;

                        Log.Error(exception, "Unhandled exception occured. {exceptionDetails}", exception?.ToString());

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("An unexpected error occured. Please try again later.");
                    });
                });

                app.UseMiddleware<RequestResponseLoggingMiddleware>();

                // Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                //{
                app.UseSwagger();
                app.UseSwaggerUI();
                //}

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
