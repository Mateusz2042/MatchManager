using DotNETCore.Repository.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManager.Models
{
    //[CollectionName("Team")]
    public class Team: Entity
    {
        public string NameTeam { get; set; }
        public Player FirstMember { get; set; }
        public Player SecondMember { get; set; }
    }
}
