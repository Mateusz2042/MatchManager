﻿using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using Application.Actors;
using Microsoft.Extensions.Configuration;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Serilog;
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

            var configurationSerilog = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

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
                loglevel=INFO,  
                loggers=[""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]
            }");

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configurationSerilog)
                .MinimumLevel.Information()
                .CreateLogger();

            Serilog.Log.Logger = logger;

            var system = ActorSystem.Create("RemoteActorSystem", config);

            var propsPlayer = Props.Create<PlayerActor>().WithRouter(new RoundRobinPool(10));
            var propsTeam = Props.Create<TeamActor>().WithRouter(new RoundRobinPool(10));
            var propsMatch = Props.Create<MatchActor>().WithRouter(new RoundRobinPool(10));

            var playerActor = system.ActorOf(propsPlayer, "playerActor");
            var teamActor = system.ActorOf(propsTeam, "teamActor");
            var matchActor = system.ActorOf(propsMatch, "matchActor");
        }

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("AkkaService stopped...");
        }
    }
}
