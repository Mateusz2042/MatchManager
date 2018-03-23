using System;
using System.Collections.Generic;
using System.Text;
using AkkaService.Messages.Player.PlayerRequest;
using FluentValidation;
using MatchManager.Models;

namespace Infrastructure.Validator.PlayerValidator
{
    public class PlayerValidatorr: AbstractValidator<CreatePlayerRequest>
    {
        public PlayerValidatorr()
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
