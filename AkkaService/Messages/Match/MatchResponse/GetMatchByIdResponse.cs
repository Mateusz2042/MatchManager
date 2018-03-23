﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Match.MatchResponse
{
    public class GetMatchByIdResponse
    {
        public GetMatchItem Match { get; }

        public GetMatchByIdResponse(GetMatchItem match)
        {
            Match = match;
        }
    }
}
