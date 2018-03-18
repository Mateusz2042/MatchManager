using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerResponse
{
    public class CreatePlayerResponse
    {
        public bool Success { get; }

        public CreatePlayerResponse(bool success)
        {
            Success = success;
        }
    }
}
