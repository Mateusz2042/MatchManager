﻿using Akka.Actor;
using AkkaService.Messages.PlayerValidator;
using AkkaService.Validator;
using FluentValidation.Results;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Actors
{
    public class PlayerActorValidator: ReceiveActor
    {
        public PlayerActorValidator()
        {
            Receive<PlayerValidatorRequest>(message => Handle(message));
        }

        public void Handle(PlayerValidatorRequest request)
        {
            var validator = new PlayerValidatorTest();
            var result = validator.Validate(request.Player);

            var validationSucceeded = result.IsValid;

            var response = new PlayerValidatorResponse(validationSucceeded);
            Sender.Tell(response);
        }
    }
}
