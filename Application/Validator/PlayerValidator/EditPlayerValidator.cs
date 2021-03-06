﻿using System;
using System.Collections.Generic;
using System.Text;
using Application.Messages.Player.PlayerRequest;
using FluentValidation;

namespace Application.Validator.PlayerValidator
{
    public class EditPlayerValidator: AbstractValidator<EditPlayerRequest>
    {
        public EditPlayerValidator()
        {
            RuleFor(player => player.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(player => player.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(player => player.NickName).NotEmpty().WithMessage("NickName is required");
            RuleFor(player => player.Age).NotEmpty().WithMessage("Age is required");
            RuleFor(player => player.Age).GreaterThan(18).WithMessage("Age must be greater than 18");
            RuleFor(player => player.Sex).NotEmpty().WithMessage("Sex is required");
        }
    }
}
