using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Match.MatchRequest
{
    public class RemoveMatchRequest
    {
        public string Id { get; set; }

        public RemoveMatchRequest(string id)
        {
            Id = id;
        }
    }
}
