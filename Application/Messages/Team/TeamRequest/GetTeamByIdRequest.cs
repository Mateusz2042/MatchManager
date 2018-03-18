using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Team.TeamRequest
{
    public class GetTeamByIdRequest
    {
        public string Id { get; }

        public GetTeamByIdRequest(string id)
        {
            Id = id;
        }
    }
}
