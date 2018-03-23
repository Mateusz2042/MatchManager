using Akka.Actor;
using Akka.Routing;
using AkkaService.Actors;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
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
            Console.WriteLine("AkkaService started...");

            var system = ActorSystem.Create("MyActor");

            var propsPlayer = Props.Create<PlayerActor>().WithRouter(new RoundRobinPool(10));
            var propsTeam = Props.Create<TeamActor>().WithRouter(new RoundRobinPool(10));
            var propsMatch = Props.Create<MatchActor>().WithRouter(new RoundRobinPool(10));

            var playerActor = system.ActorOf(propsPlayer, "playerActor");
            var teamActor = system.ActorOf(propsTeam, "teamActor");
            var matchActor = system.ActorOf(propsMatch, "matchActor");

            ActorModelWrapper.PlayerActor = playerActor;
            ActorModelWrapper.TeamActor = teamActor;
            ActorModelWrapper.MatchActor = matchActor;
        }

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("AkkaService stopped...");
        }
    }
}
