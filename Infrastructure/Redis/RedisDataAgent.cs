using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Redis
{
    public class RedisDataAgent: IRediseDataAgent
    {
        private static IDatabase _database;
        private readonly ConnectionMultiplexer _connectionString = ConnectionMultiplexer.Connect("localhost");

        public RedisDataAgent()
        {
            _database = _connectionString.GetDatabase();
        }

        public string GetStringValue(string key)
        {
            return _database.StringGet(key);
        }

        public void SetStringValue(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public void DeleteStringValue(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
