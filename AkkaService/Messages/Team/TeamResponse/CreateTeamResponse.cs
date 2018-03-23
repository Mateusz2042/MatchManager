using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Team.TeamResponse
{
    public class CreateTeamResponse
    {
        public bool Success { get; }

        public CreateTeamResponse(bool success)
        {
            Success = success;
        }
    }
}
