using AkkaService.Validator.TeamValidator;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Team.TeamRequest
{
    [Validator(typeof(EditTeamValidator))]
    public class EditTeamRequest
    {
        public string Id { get; set; }
        public string NameTeam { get; set; }
        public string IdFirstMember { get; set; }
        public string IdSecondMember { get; set; }

        public EditTeamRequest(string id, string nameTeam, string idFirstMember, string idSecondMember)
        {
            Id = id;
            NameTeam = nameTeam;
            IdFirstMember = idFirstMember;
            IdSecondMember = idSecondMember;
        }
    }
}
