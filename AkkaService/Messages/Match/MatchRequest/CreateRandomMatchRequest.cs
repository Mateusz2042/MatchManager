using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchRequest
{
    public class CreateRandomMatchRequest
    {
        public string DateTimeMatch { get; set; }

        public CreateRandomMatchRequest(string dateTimeMatch)
        {
            DateTimeMatch = dateTimeMatch;
        }
    }
}
