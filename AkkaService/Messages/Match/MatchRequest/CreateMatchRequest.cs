using FluentValidation.Attributes;
using Infrastructure.Validator.MatchValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchRequest
{
    [Validator(typeof(MatchValidator))]
    public class CreateMatchRequest
    {
        public string IdFirstTeam { get; set; }
        public string IdSecondTeam { get; set; }
        public string DateTimeMatch { get; set; }

        public CreateMatchRequest(string idFirstTeam, string idSecondTeam, string dateTimeMatch)
        {
            IdFirstTeam = idFirstTeam;
            IdSecondTeam = idSecondTeam;
            DateTimeMatch = dateTimeMatch;
        }
    }
}
