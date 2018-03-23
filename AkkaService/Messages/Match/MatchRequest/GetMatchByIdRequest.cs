using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchRequest
{
    public class GetMatchByIdRequest
    {
        public string Id { get; }

        public GetMatchByIdRequest(string id)
        {
            Id = id;
        }
    }
}
