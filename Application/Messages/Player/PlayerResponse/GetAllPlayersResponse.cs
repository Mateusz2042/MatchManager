using MatchManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Messages.Player.PlayerResponse
{   
    public class GetAllPlayersResponse //Co dostaniemy w odpowiedzi roslynator
    {
        public ICollection<GetPlayerItem> Players { get;}

        public GetAllPlayersResponse(IEnumerable<GetPlayerItem> players)
        {
            Players = players.ToList();
        }
    }

    public class GetPlayerItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public bool IsDeleted { get; set; }

        public GetPlayerItem(string id, string firstName, string lastName, string nickName, int age, Sex sex, bool isDeleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            Age = age;
            Sex = sex;
            IsDeleted = isDeleted;
        }

    }
}
