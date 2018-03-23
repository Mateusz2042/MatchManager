﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Team.TeamResponse
{
    public class EditTeamResponse
    {
        public bool Success { get; set; }

        public EditTeamResponse(bool success)
        {
            Success = success;
        }
    }
}
