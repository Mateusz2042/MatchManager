using Akka.Actor;
using Application.Messages.PlayerValidator;
using Application.Validator;
using FluentValidation.Results;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Actors
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
