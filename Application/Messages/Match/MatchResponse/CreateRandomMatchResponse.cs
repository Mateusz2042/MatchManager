using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Match.MatchResponse
{
    public class CreateRandomMatchResponse
    {
        public bool Success { get; }

        public CreateRandomMatchResponse(bool success)
        {
            Success = success;
        }
    }
}
