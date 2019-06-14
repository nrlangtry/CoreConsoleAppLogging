using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConsoleAppLogging
{
    public static partial class StartupExtensions
    {
        public static IServiceCollection AddApplicationLogger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddEventLog();
            });

            var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            Logger.AddLogger(loggerFactory);

            return services;
        }
    }

    public static class Logger
    {
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;

        private static ILogger GetLogger()
        {
            if (_logger == null)
            {
                if (_loggerFactory == null)
                    _loggerFactory = new LoggerFactory();

                _logger = _loggerFactory.CreateLogger("CoreConsoleApp");
            }

            return _logger;
        }

        internal static void AddLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static void Debug(string message)
        {
            GetLogger().LogDebug(message);
        }

        public static void Trace(string message)
        {
            GetLogger().LogTrace(message);
        }

        public static void Information(string message)
        {
            GetLogger().LogInformation(message);
        }

        public static void Warning(string message, Exception ex)
        {
            GetLogger().LogWarning(ex, message);
        }

        public static void Error(string message, Exception ex)
        {
            GetLogger().LogError(ex, message);
        }

        public static void Critical(string message, Exception ex)
        {
            GetLogger().LogCritical(ex, message);
        }
    }
}
