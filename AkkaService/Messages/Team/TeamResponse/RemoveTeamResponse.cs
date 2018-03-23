using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Team.TeamResponse
{
    public class RemoveTeamResponse
    {
        public bool Success { get; set; }

        public RemoveTeamResponse(bool success)
        {
            Success = success;
        }
    }
}
