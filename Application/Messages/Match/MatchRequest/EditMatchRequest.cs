using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Match.MatchRequest
{
    public class EditMatchRequest
    {
        public string Id { get; set; }
        public string IdFirstTeam { get; set; }
        public string IdSecondTeam { get; set; }
        public string DateTimeMatch { get; set; }
        public int ScoreOfFirstTeam { get; set; }
        public int ScoreOfSecondTeam { get; set; }

        public EditMatchRequest(string id, string idFirstTeam, string idSecondTeam, string dateTimeMatch, int scoreOfFirstTeam, int scoreOfSecondTeam)
        {
            Id = id;
            IdFirstTeam = idFirstTeam;
            IdSecondTeam = idSecondTeam;
            DateTimeMatch = dateTimeMatch;
            ScoreOfFirstTeam = scoreOfFirstTeam;
            ScoreOfSecondTeam = scoreOfSecondTeam;
        }
    }
}
