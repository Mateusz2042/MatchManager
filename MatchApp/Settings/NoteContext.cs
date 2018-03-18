using MatchManager.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchApp.Settings
{
    public class NoteContext
    {
        private readonly IMongoDatabase _database = null;

        public NoteContext(IOptions<SettingsConnection> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Player> Players
        {
            get
            {
                return _database.GetCollection<Player>("Players");
            }
        }

        public IMongoCollection<Team> Teams
        {
            get
            {
                return _database.GetCollection<Team>("Teams");
            }
        }

        public IMongoCollection<Match> Matches
        {
            get
            {
                return _database.GetCollection<Match>("Matches");
            }
        }
    }
}
