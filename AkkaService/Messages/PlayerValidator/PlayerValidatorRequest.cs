using AkkaService.Validator;
using FluentValidation.Attributes;
using MatchManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.PlayerValidator
{
    [Validator(typeof(PlayerValidatorTest))]
    public class PlayerValidatorRequest
    {
        public MatchManager.Models.Player Player { get; set; }

        public PlayerValidatorRequest(MatchManager.Models.Player player)
        {
            Player = player;
        }
    }
}
