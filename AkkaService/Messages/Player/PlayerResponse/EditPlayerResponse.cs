using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Player.PlayerResponse
{
    public class EditPlayerResponse
    {
        public bool Success { get; set; }

        public EditPlayerResponse(bool success)
        {
            Success = success;
        }
    }
}
