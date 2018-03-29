using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Redis
{
    public interface IRediseDataAgent
    {
        string GetStringValue(string key);

        void SetStringValue(string key, string value);

        void DeleteStringValue(string key);
    }
}
