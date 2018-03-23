using System;
using System.Collections.Generic;
using System.Text;
using AkkaService.Messages.Team.TeamRequest;
using FluentValidation;
using MatchManager.Models;

namespace Infrastructure.Validator.TeamValidator
{
    public class TeamValidator: AbstractValidator<CreateTeamRequest>
    {
        public TeamValidator()
        {
            RuleFor(x => x.NameTeam).NotEmpty().WithMessage("NameTeam is required");
            RuleFor(x => x.IdFirstMember).NotEmpty().WithMessage("IdFirstMember is required");
            RuleFor(x => x.IdSecondMember).NotEmpty().WithMessage("IdSecondMember is required");
        }
    }
}
