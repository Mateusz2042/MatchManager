using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PeterKottas.DotNetCore.WindowsService;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace AkkaService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceRunner<ServiceAkka>.Run(config =>
            {
                var nameService = "AkkaService";
                config.SetName(nameService);

                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory((extraArguments, controller) =>
                    {
                        return new ServiceAkka();
                    });

                    serviceConfig.OnStart((service, extraArguments) =>
                    {
                        Console.WriteLine("Service {0} started", nameService);
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        Console.WriteLine("Service {0} stopped", nameService);
                        service.Stop();
                    });

                    serviceConfig.OnInstall(service =>
                    {
                        Console.WriteLine("Service {0} installed", nameService);
                    });

                    serviceConfig.OnUnInstall(service =>
                    {
                        Console.WriteLine("Service {0} uninstalled", nameService);
                    });

                    serviceConfig.OnPause(service =>
                    {
                        Console.WriteLine("Service {0} paused", nameService);
                    });

                    serviceConfig.OnContinue(service =>
                    {
                        Console.WriteLine("Service {0} continued", nameService);
                    });

                    serviceConfig.OnError(e =>
                    {
                        Console.WriteLine("Service {0} errored with exception : {1}", nameService, e.Message);
                    });
                });
            });
        }
    }
}
