using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Team.TeamRequest
{
    public class RemoveTeamRequest
    {
        public string Id { get; set; }

        public RemoveTeamRequest(string id)
        {
            Id = id;
        }
    }
}
