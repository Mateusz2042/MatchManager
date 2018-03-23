using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchResponse
{
    public class EditMatchResponse
    {
        public bool Success { get; set; }

        public EditMatchResponse(bool success)
        {
            Success = success;
        }
    }
}
