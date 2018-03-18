using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Team.TeamResponse
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
