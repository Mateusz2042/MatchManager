using DotNETCore.Repository.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManager.Models
{
    //[CollectionName("Match")]
    public class Match: Entity // MongoRepository (by uzywać tego normalnie xD)
    {
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public string DateTimeMatch { get; set; }
        public int ScoreOfFirstTeam { get; set; }
        public int ScoreOfSecondTeam { get; set; }
    }
}
