using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Application.Actors;
using Application.Messages.Team.TeamRequest;
using Application.Messages.Team.TeamResponse;
using MatchManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Team")]
    public class TeamController : Controller
    {
        private IActorRef _teamActor;
        private IActorRef _playerActor;

        public TeamController()
        {
            _teamActor = ActorModelWrapper.TeamActor;
            _playerActor = ActorModelWrapper.PlayerActor;
        }

        [HttpGet]
        public async Task<GetAllTeamsResponse> GetAllTeams()
        {
            var request = new GetAllTeamsRequest();

            return await _teamActor.Ask<GetAllTeamsResponse>(request);
        }

        [HttpPost]
        public async Task<CreateTeamResponse> CreateTeam(string nameTeam, string idFirstMember, string idSecondMember)
        {
            var request = new CreateTeamRequest(nameTeam, idFirstMember, idSecondMember);

            return await _teamActor.Ask<CreateTeamResponse>(request);
        }

        [HttpGet("{id}")]
        public async Task<GetTeamByIdResponse> GetTeamById(string id)
        {
            var request = new GetTeamByIdRequest(id);

            var result = await _teamActor.Ask<GetTeamByIdResponse>(request);

            return result;
        }
        
        [HttpPut]
        public async Task<EditTeamResponse> EditTeam(string id, string nameTeam, string idFirstMember, string idSecondMember)
        {
            var request = new EditTeamRequest(id, nameTeam, idFirstMember, idSecondMember);

            return await _teamActor.Ask<EditTeamResponse>(request);
        }

        [HttpDelete("{id}")]
        public async Task<RemoveTeamResponse> RemoveTeam(string id)
        {
            var request = new RemoveTeamRequest(id);

            return await _teamActor.Ask<RemoveTeamResponse>(request);
        }
    }
}
