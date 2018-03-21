using Akka.Actor;
using Akka.TestKit.Xunit2;
using Application.Actors;
using Application.Messages.PlayerValidator;
using MatchManager.Enums;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.PlayerActorTest
{
    public class PlayerActorTest : TestKit
    {
        List<Player> playersFailed = new List<Player>()
        {
            new Player{FirstName = "", LastName = "Nowak", NickName = "Mat", Age = 23, Sex = Sex.Male, IsDeleted = false},
            new Player{FirstName = "Kasia", LastName = "", NickName = "Kao", Age = 22, Sex = Sex.Female, IsDeleted = false},
            new Player{FirstName = "Jan", LastName = "Nowy", NickName = "", Age = 20, Sex = Sex.Male, IsDeleted = false},
            new Player{FirstName = "Marcin", LastName = "Wojtowski", NickName = "MR", Age = 10, Sex = Sex.Male, IsDeleted = false}
        };

        [Theory]
        [InlineData("Mateusz", "Nowak", "Mat", 23, Sex.Male, false)]
        [InlineData("Kasia", "Kowalska", "Kao", 18, Sex.Female, false)]
        [InlineData("Jan", "Nowy", "Jano", 20, Sex.Male, false)]
        public void CreatePlayerActorTestPassed(string firstName, string lastName, string nickName, int age, Sex sex, bool isDeleted)
        {
            var identity = Sys.ActorOf(Props.Create(() => new PlayerActorValidator()));

            Player player = new Player { FirstName = firstName, LastName = lastName, NickName = nickName, Age = age, Sex = sex, IsDeleted = isDeleted };

            identity.Tell(new PlayerValidatorRequest(player));

            var result = ExpectMsg<PlayerValidatorResponse>().Success;

            Assert.True(result);
        }

        [Fact]
        public void CreatePlayerActorTestFailed()
        {
            var identity = Sys.ActorOf(Props.Create(() => new PlayerActorValidator()));

            foreach (var item in playersFailed)
            {
                identity.Tell(new PlayerValidatorRequest(item));

                var result = ExpectMsg<PlayerValidatorResponse>().Success;

                Assert.False(result);
            }
        }

        [Fact]
        public void Bool()
        {
            if (6 % 2 == 0)
            {
                Assert.True(true);
            }
        }
    }
}
