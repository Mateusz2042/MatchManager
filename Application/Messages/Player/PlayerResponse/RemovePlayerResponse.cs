using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerResponse
{
    public class RemovePlayerResponse
    {
        public bool Success { get; set; }

        public RemovePlayerResponse(bool success)
        {
            Success = success;
        }
    }
}
