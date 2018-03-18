using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Application.Actors;
using Application.Messages.Match.MatchRequest;
using Application.Messages.Match.MatchResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Match")]
    public class MatchController : Controller
    {
        private IActorRef _matchActor;
        private IActorRef _teamActor;

        public MatchController()
        {
            _matchActor = ActorModelWrapper.MatchActor;
            _teamActor = ActorModelWrapper.TeamActor;
        }

        [HttpGet]
        public async Task<GetAllMatchesResponse> GetAllMatches()
        {
            var request = new GetAllMatchesRequest();

            return await _matchActor.Ask<GetAllMatchesResponse>(request);
        }

        [HttpPost]
        public async Task<CreateMatchResponse> CreateMatch(string idFirstTeam, string idSecondTeam, string dateTimeMatch)
        {

            var request = new CreateMatchRequest(idFirstTeam, idSecondTeam, dateTimeMatch);

            return await _matchActor.Ask<CreateMatchResponse>(request);
        }

        [HttpGet("{id}")]
        public async Task<GetMatchByIdResponse> GetMatchById(string id)
        {
            var request = new GetMatchByIdRequest(id);

            var result = await _matchActor.Ask<GetMatchByIdResponse>(request);

            return result;
        }

        [HttpPut]
        public async Task<EditMatchResponse> EditMatch(string id, string idFirstTeam, string idSecondTeam, string dateTimeMatch, int scoreOfFirstTeam, int scoreOfSecondTeam)
        {
            var request = new EditMatchRequest(id, idFirstTeam, idSecondTeam, dateTimeMatch, scoreOfFirstTeam, scoreOfSecondTeam);

            return await _matchActor.Ask<EditMatchResponse>(request);
        }

        [HttpDelete("{id}")]
        public async Task<RemoveMatchResponse> RemoveMatch(string id)
        {
            var request = new RemoveMatchRequest(id);

            return await _matchActor.Ask<RemoveMatchResponse>(request);
        }
    }
}
