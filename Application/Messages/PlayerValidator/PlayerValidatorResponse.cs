using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.PlayerValidator
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
