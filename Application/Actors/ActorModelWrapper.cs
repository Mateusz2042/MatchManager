using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actors
{
    public static class ActorModelWrapper
    {
        public static IActorRef PlayerActor { get; set; }
    }
}
