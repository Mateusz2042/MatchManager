using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Match.MatchResponse
{
    public class RemoveMatchResponse
    {
        public bool Success { get; set; }

        public RemoveMatchResponse(bool success)
        {
            Success = success;
        }
    }
}
