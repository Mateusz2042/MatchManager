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
        List<Player> playersPassed = new List<Player>()
        {
            new Player{FirstName = "Mateusz", LastName = "Nowak", NickName = "Mat", Age = 23, Sex = Sex.Male, IsDeleted = false},
            new Player{FirstName = "Kasia", LastName = "Kowalska", NickName = "Kao", Age = 22, Sex = Sex.Female, IsDeleted = false},
            new Player{FirstName = "Jan", LastName = "Nowy", NickName = "Jano", Age = 20, Sex = Sex.Male, IsDeleted = false},
        };

        List<Player> playersFailed = new List<Player>()
        {
            new Player{FirstName = "", LastName = "Nowak", NickName = "Mat", Age = 23, Sex = Sex.Male, IsDeleted = false},
            new Player{FirstName = "Kasia", LastName = "", NickName = "Kao", Age = 22, Sex = Sex.Female, IsDeleted = false},
            new Player{FirstName = "Jan", LastName = "Nowy", NickName = "", Age = 20, Sex = Sex.Male, IsDeleted = false},
            new Player{FirstName = "Marcin", LastName = "Wojtowski", NickName = "MR", Age = 10, Sex = Sex.Male, IsDeleted = false}
        };

        [Fact]
        public void CreatePlayerActorTestPassed()
        {
            var identity = Sys.ActorOf(Props.Create(() => new PlayerActorValidator()));

            foreach (var item in playersPassed)
            {
                identity.Tell(new PlayerValidatorRequest(item));

                var result = ExpectMsg<PlayerValidatorResponse>().Success;

                Assert.True(result);
            }
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
