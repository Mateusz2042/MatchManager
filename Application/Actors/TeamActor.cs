using Akka.Actor;
using Application.Messages.Team.TeamRequest;
using Application.Messages.Team.TeamResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actors
{
    public class TeamActor: ReceiveActor
    {
        public TeamActor()
        {
            Receive<GetAllTeamsRequest>(message => Handle(message));
            Receive<GetTeamByIdRequest>(message => Handle(message));
            Receive<CreateTeamByIdRequest>(message => Handle(message));
            Receive<RemoveTeamRequest>(message => Handle(message));
            Receive<EditTeamRequest>(message => Handle(message));
        }

        public void Handle(GetAllTeamsRequest request)
        {
            var response = new GetAllTeamsResponse();
            Sender.Tell(response);
        }

        public void Handle(GetTeamByIdRequest request)
        {
            var response = new GetTeamByIdResponse();
            Sender.Tell(response);
        }

        public void Handle(CreateTeamByIdRequest request)
        {
            var response = new CreateTeamResponse();
            Sender.Tell(response);
        }

        public void Handle(RemoveTeamRequest request)
        {
            var response = new RemoveTeamResponse();
            Sender.Tell(response);
        }

        public void Handle(EditTeamRequest request)
        {
            var response = new EditTeamResponse();
            Sender.Tell(response);
        }
    }
}
