using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchResponse
{
    public class CreateMatchResponse
    {
        public bool Success { get; }

        public CreateMatchResponse(bool success)
        {
            Success = success;
        }
    }
}
