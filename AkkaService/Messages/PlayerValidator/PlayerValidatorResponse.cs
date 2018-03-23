using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.PlayerValidator
{
    public class PlayerValidatorResponse
    {
        public bool Success { get; set; }

        public PlayerValidatorResponse(bool success)
        {
            Success = success;
        }
    }
}
