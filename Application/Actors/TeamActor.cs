using Akka.Actor;
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
            }
            catch (Exception)
            {

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
            }
            catch (Exception)
            {

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
                    }
                }
                else
                {
                    var response = new CreateTeamResponse(false);
                    Sender.Tell(response);
                }
            }
            catch (Exception)
            {
                var response = new CreateTeamResponse(false);
                Sender.Tell(response);
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
            }
            catch (Exception)
            {
                var response = new RemoveTeamResponse(false);
                Sender.Tell(response);
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
            }
            catch (Exception)
            {
                var response = new EditTeamResponse(false);
                Sender.Tell(response);
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
