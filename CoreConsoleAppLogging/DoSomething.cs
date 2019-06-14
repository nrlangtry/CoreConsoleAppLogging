using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConsoleAppLogging
{
    public interface IDoSomething
    {
        void BeProductive();
    }

    public class DoSomething : IDoSomething
    {
        readonly ILogger<DoSomething> _logger;

        public DoSomething(ILogger<DoSomething> logger)
        {
            _logger = logger;
        }

        public void BeProductive()
        {
            _logger?.LogInformation("Testing DI");

            Logger.Information("Testing static Logger");
        }
    }
}
