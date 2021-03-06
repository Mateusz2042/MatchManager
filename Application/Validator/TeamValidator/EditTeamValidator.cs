﻿using System;
using System.Collections.Generic;
using System.Text;
using Application.Messages.Team.TeamRequest;
using FluentValidation;

namespace Application.Validator.TeamValidator
{
    public class EditTeamValidator: AbstractValidator<EditTeamRequest>
    {
        public EditTeamValidator()
        {
            RuleFor(x => x.NameTeam).NotEmpty().WithMessage("NameTeam is required");
            RuleFor(x => x.IdFirstMember).NotEmpty().WithMessage("IdFirstMember is required");
            RuleFor(x => x.IdSecondMember).NotEmpty().WithMessage("IdSecondMember is required");
        }
    }
}
