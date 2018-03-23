using System;
using System.Collections.Generic;
using System.Text;
using AkkaService.Messages.Match.MatchRequest;
using FluentValidation;
using MatchManager.Models;

namespace Infrastructure.Validator.MatchValidator
{
    public class MatchValidator: AbstractValidator<CreateMatchRequest>
    {
        public MatchValidator()
        {
            RuleFor(match => match.IdFirstTeam).NotEmpty().WithMessage("IdFirstTeam is required");
            RuleFor(match => match.IdSecondTeam).NotEmpty().WithMessage("IdSecondTeam is required");
            RuleFor(match => match.DateTimeMatch).NotEmpty().WithMessage("DateTimeMatch is required");
            //RuleFor(match => match.ScoreOfFirstTeam).GreaterThan(0).WithMessage("ScoreOfFirstTeam must be greater than 0");
            //RuleFor(match => match.ScoreOfSecondTeam).GreaterThan(0).WithMessage("ScoreOfSecondTeam must be greater than 0");
        }
    }
}
