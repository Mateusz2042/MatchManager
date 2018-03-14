using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManager.Models
{
    public class Team
    {
        public ObjectId TeamId { get; set; }
        public Player FirstMember { get; set; }
        public Player SecondMember { get; set; }
    }
}
