using System;
using System.Collections.Generic;
using System.Text;
using AkkaService.Messages.PlayerValidator;
using FluentValidation;
using MatchManager.Models;

namespace AkkaService.Validator
{
    public class PlayerValidatorTest: AbstractValidator<Player>
    {
        public PlayerValidatorTest()
        {
            RuleFor(player => player.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(player => player.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(player => player.NickName).NotEmpty().WithMessage("NickName is required");
            RuleFor(player => player.Age).NotNull().WithMessage("Age is required");
            RuleFor(player => player.Age).GreaterThanOrEqualTo(18).WithMessage("Age must be greater than or equal to 18");
            RuleFor(player => player.Sex).NotNull().WithMessage("Sex is required");
        }
    }
}
