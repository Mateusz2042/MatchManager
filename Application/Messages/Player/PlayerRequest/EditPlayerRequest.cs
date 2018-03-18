using Application.Messages.Player.PlayerResponse;
using MatchManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Player.PlayerRequest
{
    public class EditPlayerRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }

        public EditPlayerRequest(string id, string firstName, string lastName, string nickName, int age, Sex sex)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            Age = age;
            Sex = sex;
        }
    }

    public class PlayerItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }

        public PlayerItem(string firstName, string lastName, string nickName, int age, Sex sex)
        {
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            Age = age;
            Sex = sex;
        }

    }

}
