using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManager.Models
{
    public class Match
    {
        public ObjectId MatchId { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public int ScoreOfFirstTeam { get; set; }
        public int ScoreOfSecondTeam { get; set; }
    }
}
