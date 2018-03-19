using FluentValidation.Attributes;
using Infrastructure.Validator.TeamValidator;
using MatchManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Team.TeamRequest
{
    [Validator(typeof(TeamValidator))]
    public class CreateTeamRequest
    {
        public string NameTeam { get; set; }
        public string IdFirstMember { get; set; }
        public string IdSecondMember { get; set; }

        public CreateTeamRequest(string nameTeam, string idFirstMember, string idSecondMember)
        {
            NameTeam = nameTeam;
            IdFirstMember = idFirstMember;
            IdSecondMember = idSecondMember;
        }
    }
}
