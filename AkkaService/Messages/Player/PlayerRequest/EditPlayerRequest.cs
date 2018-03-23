using AkkaService.Messages.Player.PlayerResponse;
using AkkaService.Validator.PlayerValidator;
using FluentValidation.Attributes;
using MatchManager.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkkaService.Messages.Player.PlayerRequest
{
    [Validator(typeof(EditPlayerValidator))]
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
