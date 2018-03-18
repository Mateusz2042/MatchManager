using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Team.TeamResponse
{
    public class GetTeamByIdResponse
    {
        public GetTeamItem Team { get; }

        public GetTeamByIdResponse(GetTeamItem team)
        {
            Team = team;
        }
    }
}
