using Akka.Actor;
using Application.Messages.Match.MatchRequest;
using Application.Messages.Match.MatchResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actors
{
    public class MatchActor: ReceiveActor
    {
        public MatchActor()
        {
            Receive<GetAllMatchesRequest>(message => Handle(message));
            Receive<GetMatchByIdRequest>(message => Handle(message));
            Receive<CreateMatchRequest>(message => Handle(message));
            Receive<RemoveMatchRequest>(message => Handle(message));
            Receive<EditMatchRequest>(message => Handle(message));
        }

        public void Handle(GetAllMatchesRequest request)
        {
            var response = new GetAllMatchesResponse();
            Sender.Tell(response);
        }

        public void Handle(GetMatchByIdRequest request)
        {
            var response = new GetMatchByIdResponse();
            Sender.Tell(response);
        }

        public void Handle(CreateMatchRequest request)
        {
            var response = new CreateMatchResponse();
            Sender.Tell(response);
        }

        public void Handle(RemoveMatchRequest request)
        {
            var response = new RemoveMatchResponse();
            Sender.Tell(response);
        }

        public void Handle(EditMatchRequest request)
        {
            var response = new EditMatchResponse();
            Sender.Tell(response);
        }
    }
}
