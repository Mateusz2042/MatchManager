using Akka.Actor;
using Akka.Event;
using Akka.Logger.Serilog;
using Application.Messages.Team.TeamRequest;
using Application.Messages.Team.TeamResponse;
using Application.Specifications.TeamSpecifications;
using DotNETCore.Repository.Mongo;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Actors
{
    public class TeamActor: ReceiveActor
    {
        private readonly IRepository<Team> _teamRepo;
        private readonly IRepository<Player> _playerRepo;
        private readonly ILoggingAdapter _logger = Context.GetLogger<SerilogLoggingAdapter>();

        public TeamActor()
        {
            _teamRepo = new Repository<Team>("mongodb://localhost:27017/MatchDatabase");
            _playerRepo = new Repository<Player>("mongodb://localhost:27017/MatchDatabase");

            Receive<GetAllTeamsRequest>(message => Handle(message));
            Receive<GetTeamByIdRequest>(message => Handle(message));
            Receive<CreateTeamRequest>(message => Handle(message));
            Receive<RemoveTeamRequest>(message => Handle(message));
            Receive<EditTeamRequest>(message => Handle(message));
        }

        public void Handle(GetAllTeamsRequest request)
        {
            try
            {
                var notDeleted = new GetTeamsNotDeletedSpecifications();

                var teams = _teamRepo.FindAll().Select(x => new GetTeamItem(x.Id, x.NameTeam, x.FirstMember, x.SecondMember, x.IsDeleted));

                var response = new GetAllTeamsResponse(teams);
                Sender.Tell(response);

                _logger.Info("Get All Teams");
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get all Teams: {0}", ex.Message);
                throw;
            }
        }

        public void Handle(GetTeamByIdRequest request)
        {
            try
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Team, GetTeamItem>();
                });

                var mapper = config.CreateMapper();

                var player = mapper.Map<GetTeamItem>(_teamRepo.Get(request.Id));

                var response = new GetTeamByIdResponse(player);
                Sender.Tell(response);

                _logger.Info("Get Team by Id: {0}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get Team by id: {0} : {1}", request.Id, ex.Message);
                throw;
            }
        }

        public void Handle(CreateTeamRequest request)
        {
            try
            {
                if (!String.IsNullOrEmpty(request.NameTeam) || !String.IsNullOrEmpty(request.IdFirstMember) || !String.IsNullOrEmpty(request.IdSecondMember))
                {
                    if (_teamRepo.Any(x => x.NameTeam.Contains(request.NameTeam)))
                    {
                        var response = new CreateTeamResponse(false);
                        Sender.Tell(response);

                        _logger.Error("Couldn't create Team: {0}: This name is exists", request.NameTeam);
                    }
                    else
                    {
                        _teamRepo.Insert(new Team
                        {
                            NameTeam = request.NameTeam,
                            FirstMember = GetPlayerById(request.IdFirstMember),
                            SecondMember = GetPlayerById(request.IdSecondMember),
                            IsDeleted = false
                        });

                        var response = new CreateTeamResponse(true);
                        Sender.Tell(response);

                        _logger.Info("Create Team successfull: {0}", request.NameTeam);
                    }
                }
                else
                {
                    var response = new CreateTeamResponse(false);
                    Sender.Tell(response);

                    _logger.Error("Couldn't create Team: {0} {1}: All fields are required", request.NameTeam);
                }
            }
            catch (Exception ex)
            {
                var response = new CreateTeamResponse(false);
                Sender.Tell(response);

                _logger.Error("Couldn't create Team: {0} {1}: ", request.NameTeam, ex.Message);

            }
        }

        public void Handle(RemoveTeamRequest request)
        {
            try
            {
                //_teamRepo.Delete(request.Id);

                var team = _teamRepo.Get(request.Id);

                team.IsDeleted = true;

                _teamRepo.Replace(team);

                var response = new RemoveTeamResponse(true);
                Sender.Tell(response);

                _logger.Info("Remove Team successfull: {0} {1}", team.NameTeam);
            }
            catch (Exception ex)
            {
                var response = new RemoveTeamResponse(false);
                Sender.Tell(response);

                _logger.Error("Couldn't remove Team by id: {0}: {1}", request.Id, ex.Message);
            }
        }

        public void Handle(EditTeamRequest request)
        {
            try
            {
                var team = _teamRepo.Get(request.Id);

                team.NameTeam = request.NameTeam;
                team.FirstMember = GetPlayerById(request.IdFirstMember);
                team.SecondMember = GetPlayerById(request.IdSecondMember);

                _teamRepo.Replace(team);
                var response = new EditTeamResponse(true);
                Sender.Tell(response);

                _logger.Info("Edit Team successfull: {0} {1}", team.NameTeam);
            }
            catch (Exception ex)
            {
                var response = new EditTeamResponse(false);
                Sender.Tell(response);

                _logger.Error("Couldn't Team by id: {0}: {1}", request.Id, ex.Message);
            }
        }

        #region Data

        public Player GetPlayerById(string id)
        {
            return _playerRepo.Get(id);
        }

        #endregion
    }
}
