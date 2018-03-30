using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Application.Actors;
using Application.Messages.Match.MatchRequest;
using Application.Messages.Match.MatchResponse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Match")]
    [EnableCors("AllowApi")]
    public class MatchController : Controller
    {
        private IActorRef _matchActor;
        private IActorRef _teamActor;

        public MatchController()
        {
            _matchActor = ActorModelWrapper.MatchActor;
            _teamActor = ActorModelWrapper.TeamActor;
        }

        [ResponseCache(Duration = 60)]
        [HttpGet]
        public async Task<GetAllMatchesResponse> GetAllMatches()
        {
            var request = new GetAllMatchesRequest();

            return await _matchActor.Ask<GetAllMatchesResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpPost]
        public async Task<CreateMatchResponse> CreateMatch(string idFirstTeam, string idSecondTeam, string dateTimeMatch)
        {
            var request = new CreateMatchRequest(idFirstTeam, idSecondTeam, dateTimeMatch);

            return await _matchActor.Ask<CreateMatchResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("{id}")]
        public async Task<GetMatchByIdResponse> GetMatchById(string id)
        {
            var request = new GetMatchByIdRequest(id);

            var result = await _matchActor.Ask<GetMatchByIdResponse>(request);

            return result;
        }

        [ResponseCache(Duration = 60)]
        [HttpPut]
        public async Task<EditMatchResponse> EditMatch(string id, string idFirstTeam, string idSecondTeam, string dateTimeMatch, int scoreOfFirstTeam, int scoreOfSecondTeam)
        {
            var request = new EditMatchRequest(id, idFirstTeam, idSecondTeam, dateTimeMatch, scoreOfFirstTeam, scoreOfSecondTeam);

            return await _matchActor.Ask<EditMatchResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [HttpDelete("{id}")]
        public async Task<RemoveMatchResponse> RemoveMatch(string id)
        {
            var request = new RemoveMatchRequest(id);

            return await _matchActor.Ask<RemoveMatchResponse>(request);
        }

        [ResponseCache(Duration = 60)]
        [Route("/CreateRandomMatch")]
        [HttpPost]
        public async Task<CreateRandomMatchResponse> CreateRandomMatch(string dateTimeMatch)
        {
            var request = new CreateRandomMatchRequest(dateTimeMatch);

            return await _matchActor.Ask<CreateRandomMatchResponse>(request);
        }
    }
}
