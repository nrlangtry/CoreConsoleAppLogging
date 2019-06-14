using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CoreConsoleAppLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddScoped<IDoSomething, DoSomething>();

            var configuration = GetConfiguration();

            serviceCollection.AddApplicationLogger(configuration);

            serviceCollection.AddSingleton<IConfiguration>(configuration);

            Logger.Information("Hello Logging!");

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var doSomethingService = serviceProvider.GetService<IDoSomething>();
            doSomethingService.BeProductive();

            Console.ReadLine();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            return configuration;
        }
    }
}
