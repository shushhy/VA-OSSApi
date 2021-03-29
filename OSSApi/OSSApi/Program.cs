using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace OSSApi {
    public class Program {
        public static void Main(string[] args) {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt", fileSizeLimitBytes: 1_000_000, rollOnFileSizeLimit: true)
                .WriteTo.Console()
                .WriteTo.Seq("https://localhost:5001")
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .CreateLogger();
            try {
                Log.Information("Starting the application");
                Log.Information("Hello, {Name}!", Environment.UserName);
                CreateHostBuilder(args).Build().Run();
            } catch (Exception e) {
                Log.Fatal(e, "Application terminated");
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
