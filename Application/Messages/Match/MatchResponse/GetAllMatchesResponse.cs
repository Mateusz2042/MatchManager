using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Messages.Match.MatchResponse
{
    public class GetAllMatchesResponse
    {
        public ICollection<GetMatchItem> Teams { get; }

        public GetAllMatchesResponse(IEnumerable<GetMatchItem> teams)
        {
            Teams = teams.ToList();
        }
    }

    public class GetMatchItem
    {
        public string Id { get; set; }
        public MatchManager.Models.Team FirstTeam { get; set; }
        public MatchManager.Models.Team SecondTeam { get; set; }
        public string DateTimeMatch { get; set; }
        public int ScoreOfFirstTeam { get; set; }
        public int ScoreOfSecondTeam { get; set; }

        public GetMatchItem(string id, MatchManager.Models.Team firstTeam, MatchManager.Models.Team secondTeam, string dateTimeMatch, int scoreOfFirstTeam, int scoreOfSecondTeam)
        {
            Id = id;
            FirstTeam = firstTeam;
            SecondTeam = secondTeam;
            DateTimeMatch = dateTimeMatch;
            ScoreOfFirstTeam = scoreOfFirstTeam;
            ScoreOfSecondTeam = scoreOfSecondTeam;
        }
    }
}
