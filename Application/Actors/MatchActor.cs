using Akka.Actor;
using Akka.Event;
using Akka.Logger.Serilog;
using Application.Messages.Match.MatchRequest;
using Application.Messages.Match.MatchResponse;
using Application.Messages.Team.TeamResponse;
using Application.Specifications.MatchSpecifications;
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
        private readonly ILoggingAdapter _logger = Context.GetLogger<SerilogLoggingAdapter>();

        public MatchActor()
        {
            _matchRepo = new Repository<Match>("mongodb://localhost:27017/MatchDatabase");
            _teamRepo = new Repository<Team>("mongodb://localhost:27017/MatchDatabase");

            Receive<GetAllMatchesRequest>(message => Handle(message));
            Receive<GetMatchByIdRequest>(message => Handle(message));
            Receive<CreateMatchRequest>(message => Handle(message));
            Receive<RemoveMatchRequest>(message => Handle(message));
            Receive<EditMatchRequest>(message => Handle(message));
            Receive<CreateRandomMatchRequest>(message => Handle(message));
        }

        public void Handle(GetAllMatchesRequest request)
        {
            try
            {
                var notDeleted = new GetMatchesNotDeletedSpecifications();

                var matches = _matchRepo.Find(notDeleted).Select(x => new GetMatchItem(x.Id, x.FirstTeam, x.SecondTeam, x.DateTimeMatch, x.ScoreOfFirstTeam, x.ScoreOfSecondTeam, x.IsDeleted)).OrderBy(x => Convert.ToDateTime(x.DateTimeMatch));

                var response = new GetAllMatchesResponse(matches);
                Sender.Tell(response);

                _logger.Info("Get All Matches");
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get all Matches: {0}", ex);
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

                _logger.Info("Get Match by Id: {0}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get Match by id: {0} : {1}", request.Id, ex);
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
                        ScoreOfSecondTeam = 0,
                        IsDeleted = false
                    });

                    var response = new CreateMatchResponse(true);
                    Sender.Tell(response);
                    _logger.Info("Create Match successfull: {0} {1}", request.IdFirstTeam, request.IdSecondTeam);
                }
                else
                {
                    var response = new CreateMatchResponse(false);
                    Sender.Tell(response);
                    _logger.Error("Couldn't create Match: {0} {1}: All fields are required", request.IdFirstTeam, request.IdSecondTeam);
                }
            }
            catch (Exception ex)
            {
                var response = new CreateMatchResponse(false);
                Sender.Tell(response);
                _logger.Error("Couldn't create Match: {0} {1}: {2}", request.IdFirstTeam, request.IdSecondTeam, ex.Message);
            }
        }

        public void Handle(RemoveMatchRequest request)
        {
            try
            {
                //_matchRepo.Delete(request.Id);

                var match = _matchRepo.Get(request.Id);

                match.IsDeleted = true;

                _matchRepo.Replace(match);

                var response = new RemoveMatchResponse(true);
                Sender.Tell(response);

                _logger.Info("Remove Match successfull: {0} {1}", match.FirstTeam, match.SecondTeam);
            }
            catch (Exception ex)
            {
                var response = new RemoveMatchResponse(false);
                Sender.Tell(response);
                _logger.Error("Couldn't remove Match: {0}:", request.Id, ex.Message);
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

                _logger.Info("Edit Match successfull: {0} {1}", match.FirstTeam, match.SecondTeam);
            }
            catch (Exception ex)
            {
                var response = new EditMatchResponse(false);
                Sender.Tell(response);

                _logger.Error("Couldn't edit Match: {0}: {1}", request.Id, ex.Message);
            }
        }

        public void Handle(CreateRandomMatchRequest request)
        {
            try
            {
                if (!String.IsNullOrEmpty(request.DateTimeMatch.ToString()))
                {
                    var firstTeam = GetRandomTeam();
                    var secondTeam = GetRandomTeam();

                    while (firstTeam.Id == secondTeam.Id)
                    {
                        secondTeam = GetRandomTeam();
                    }

                    _matchRepo.Insert(new Match
                    {
                        FirstTeam = GetTeamById(firstTeam.Id),
                        SecondTeam = GetTeamById(secondTeam.Id),
                        DateTimeMatch = request.DateTimeMatch,
                        ScoreOfFirstTeam = 0,
                        ScoreOfSecondTeam = 0,
                        IsDeleted = false
                    });

                    var response = new CreateRandomMatchResponse(true);
                    Sender.Tell(response);

                    _logger.Info("Create Random Match successfull: {0}", request.DateTimeMatch);
                }
                else
                {
                    var response = new CreateRandomMatchResponse(false);
                    Sender.Tell(response);

                    _logger.Info("Couldn't create Random Match: {0}: All fields are required", request.DateTimeMatch);
                }
            }
            catch (Exception ex)
            {
                var response = new CreateRandomMatchResponse(false);
                Sender.Tell(response);
                _logger.Info("Couldn't create Random Match: {0}: {1}", request.DateTimeMatch, ex.Message);
            }
        }

        #region Data

        public Team GetTeamById(string id)
        {
            return _teamRepo.Get(id);
        }

        public GetTeamItem GetRandomTeam()
        {
            var teams = _teamRepo.FindAll().Select(x => new GetTeamItem(x.Id, x.NameTeam, x.FirstMember, x.SecondMember, x.IsDeleted)).ToArray();

            Random rand = new Random();

            var y = rand.Next(0, teams.Length);

            return teams[y];
        }

        #endregion

    }
}
