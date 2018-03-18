using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Match.MatchRequest
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
