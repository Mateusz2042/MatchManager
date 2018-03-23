using PeterKottas.DotNetCore.WindowsService;
using System;

namespace AkkaService
{
    class Program
    {
        static void Main(string[] args)
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
