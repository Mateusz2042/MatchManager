using DotNETCore.Repository.Mongo;
using MatchManager.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchManager.Models
{
    //[CollectionName("Player")]
    public class Player: Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
    }
}
