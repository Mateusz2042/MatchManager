using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Routing;
using Application.Actors;
using Autofac;
using Microsoft.Extensions.Configuration;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AkkaService
{
    public class ServiceAkka : MicroService, IMicroService
    {
        private IMicroServiceController Controller;

        public ServiceAkka()
        {
            Controller = null;
        }

        public ServiceAkka(IMicroServiceController controller)
        {
            Controller = controller;
        }
        
        public void Start()
        {
            this.StartBase();

            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            string configurationAkka = configuration.GetSection("configurationAkka").Value;

            Console.WriteLine("AkkaService started...");

            var config = ConfigurationFactory.ParseString(@"akka {  
                actor {
                    provider = remote
                }
                remote {
                    dot-netty.tcp {
                        port = 8090 #bound to a specific port
                        hostname = localhost
                    }
                }
            }");

            var builderAutoFac = new Autofac.ContainerBuilder();
            builderAutoFac.RegisterType<PlayerActor>();
            builderAutoFac.RegisterType<TeamActor>();
            builderAutoFac.RegisterType<MatchActor>();
            var container = builderAutoFac.Build();

            var system = ActorSystem.Create("RemoteActorSystem", config);

            var resolver = new AutoFacDependencyResolver(container, system);

            //var propsPlayer = Props.Create<PlayerActor>().WithRouter(new RoundRobinPool(10));
            //var propsTeam = Props.Create<TeamActor>().WithRouter(new RoundRobinPool(10));
            //var propsMatch = Props.Create<MatchActor>().WithRouter(new RoundRobinPool(10));

            var playerActor = system.ActorOf(resolver.Create<PlayerActor>().WithRouter(new RoundRobinPool(10)), "playerActor");
            var teamActor = system.ActorOf(resolver.Create<TeamActor>().WithRouter(new RoundRobinPool(10)), "teamActor");
            var matchActor = system.ActorOf(resolver.Create<MatchActor>().WithRouter(new RoundRobinPool(10)), "matchActor");
        }

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("AkkaService stopped...");
        }
    }
}
