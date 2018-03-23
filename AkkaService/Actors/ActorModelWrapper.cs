using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Actors
{
    public static class ActorModelWrapper
    {
        public static IActorRef PlayerActor { get; set; }
        public static IActorRef TeamActor { get; set; }
        public static IActorRef MatchActor { get; set; }
    }
}
