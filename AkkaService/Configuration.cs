using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AkkaService
{
    public class Configuration
    {
        public Logger Logger { get; set; }

        public Configuration()
        {
            Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            // Add this line:
            .WriteTo.RollingFile(
                Path.Combine("Logs\\log-{Date}.txt"))
                .CreateLogger();
        }
    }
}
