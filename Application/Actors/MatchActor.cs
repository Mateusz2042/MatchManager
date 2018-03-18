using Akka.Actor;
using Application.Messages.Match.MatchRequest;
using Application.Messages.Match.MatchResponse;
using DotNETCore.Repository.Mongo;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Actors
{
    public class MatchActor: ReceiveActor
    {
        private readonly IRepository<Match> _matchRepo;
        private readonly IRepository<Team> _teamRepo;

        public MatchActor()
        {
            _matchRepo = new Repository<Match>("mongodb://localhost:27017/MatchDatabase");
            _teamRepo = new Repository<Team>("mongodb://localhost:27017/MatchDatabase");

            Receive<GetAllMatchesRequest>(message => Handle(message));
            Receive<GetMatchByIdRequest>(message => Handle(message));
            Receive<CreateMatchRequest>(message => Handle(message));
            Receive<RemoveMatchRequest>(message => Handle(message));
            Receive<EditMatchRequest>(message => Handle(message));
        }

        public void Handle(GetAllMatchesRequest request)
        {
            try
            {
                var matches = _matchRepo.FindAll().Select(x => new GetMatchItem(x.Id, x.FirstTeam, x.SecondTeam, x.DateTimeMatch, x.ScoreOfFirstTeam, x.ScoreOfSecondTeam));

                var response = new GetAllMatchesResponse(matches);
                Sender.Tell(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Handle(GetMatchByIdRequest request)
        {
            try
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Match, GetMatchItem>();
                });

                var mapper = config.CreateMapper();

                var match = mapper.Map<GetMatchItem>(_matchRepo.Get(request.Id));

                var response = new GetMatchByIdResponse(match);
                Sender.Tell(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Handle(CreateMatchRequest request)
        {
            try
            {
                if (!String.IsNullOrEmpty(request.IdFirstTeam) || !String.IsNullOrEmpty(request.IdSecondTeam) || !String.IsNullOrEmpty(request.DateTimeMatch.ToString()))
                {
                    _matchRepo.Insert(new Match
                    {
                        FirstTeam = GetTeamById(request.IdFirstTeam),
                        SecondTeam = GetTeamById(request.IdSecondTeam),
                        DateTimeMatch = request.DateTimeMatch,
                        ScoreOfFirstTeam = 0,
                        ScoreOfSecondTeam = 0
                    });

                    var response = new CreateMatchResponse(true);
                    Sender.Tell(response);
                }
                else
                {
                    var response = new CreateMatchResponse(false);
                    Sender.Tell(response);
                }
            }
            catch (Exception)
            {
                var response = new CreateMatchResponse(false);
                Sender.Tell(response);
            }
        }

        public void Handle(RemoveMatchRequest request)
        {
            try
            {
                _matchRepo.Delete(request.Id);

                var response = new RemoveMatchResponse(true);
                Sender.Tell(response);
            }
            catch (Exception)
            {
                var response = new RemoveMatchResponse(false);
                Sender.Tell(response);
            }
        }

        public void Handle(EditMatchRequest request)
        {
            try
            {
                var match = _matchRepo.Get(request.Id);

                match.FirstTeam = GetTeamById(request.IdFirstTeam);
                match.SecondTeam = GetTeamById(request.IdSecondTeam);
                match.DateTimeMatch = request.DateTimeMatch;
                match.ScoreOfFirstTeam = request.ScoreOfFirstTeam;
                match.ScoreOfSecondTeam = request.ScoreOfSecondTeam;

                _matchRepo.Replace(match);
                var response = new EditMatchResponse(true);
                Sender.Tell(response);
            }
            catch (Exception)
            {
                var response = new EditMatchResponse(false);
                Sender.Tell(response);
            }
        }
        #region Data

        public Team GetTeamById(string id)
        {
            return _teamRepo.Get(id);
        }

        #endregion

    }
}
