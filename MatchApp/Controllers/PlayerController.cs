using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Actor.Dsl;
using Application.Actors;
using Application.Messages.Player.PlayerRequest;
using Application.Messages.Player.PlayerResponse;
using Hangfire;
using MatchManager.Enums;
using MatchManager.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MatchApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Player")]
    [EnableCors("AllowApi")]
    public class PlayerController : Controller
    {
        private IActorRef _playerActor;

        public PlayerController()
        {
            _playerActor = ActorModelWrapper.PlayerActor;
            BackgroundJob.Schedule(() => GetAllPlayers(), TimeSpan.FromHours(8));
            RecurringJob.AddOrUpdate(() => GetAllPlayers(), " 5 * * * * ");
        }

        public void TestHangfire()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet]
        public async Task<GetAllPlayersResponse> GetAllPlayers()
        {
            //var actorRef = _actorSystem.ActorOf(Props.Create<PlayerActor>());

            BackgroundJob.Schedule(() => TestHangfire(), TimeSpan.FromSeconds(30));

            var request = new GetAllPlayersRequest();

            return await _playerActor.Ask<GetAllPlayersResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpPost]
        public async Task<CreatePlayerResponse> CreatePlayer(string firstName, string lastName, string nickName, int age, Sex sex)
        {
            //var actorRef = _actorSystem.ActorOf(Props.Create<PlayerActor>());

            var request = new CreatePlayerRequest(firstName, lastName, nickName, age, sex);

            return await _playerActor.Ask<CreatePlayerResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("{id}")]
        public async Task<GetPlayerByIdResponse> GetPlayerById(string id)
        {
            //var actorRef = _actorSystem.ActorOf(Props.Create<PlayerActor>());

            var request = new GetPlayerByIdRequest(id);

            var result = await _playerActor.Ask<GetPlayerByIdResponse>(request);

            return result;
        }

        [ResponseCache(Duration = 60)]
        [HttpPut]
        public async Task<EditPlayerResponse> EditPlayer(string id, string firstName, string lastName, string nickName, int age, Sex sex)
        {
            //var actorRef = _actorSystem.ActorOf(Props.Create<PlayerActor>());

            var request = new EditPlayerRequest(id, firstName, lastName, nickName, age, sex);

            return await _playerActor.Ask<EditPlayerResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpDelete("{id}")]
        public async Task<RemovePlayerResponse> RemovePlayer(string id)
        {
            //var actorRef = _actorSystem.ActorOf(Props.Create<PlayerActor>());

            var request = new RemovePlayerRequest(id);

            return await _playerActor.Ask<RemovePlayerResponse>(request);
        }
    }
}
