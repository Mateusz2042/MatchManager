using Akka.Actor;
using Akka.Event;
using Akka.Logger.Serilog;
using Application.Messages.Player.PlayerRequest;
using Application.Messages.Player.PlayerResponse;
using Application.Specifications.PlayerSpecifications;
using AutoMapper;
using DotNETCore.Repository.Mongo;
using MatchManager.Enums;
using MatchManager.Models;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Application.Actors
{
    public class PlayerActor: ReceiveActor // utworzenie PlayerActora
    {
        private readonly IRepository<Player> _playerRepo;
        private readonly ILoggingAdapter _logger = Context.GetLogger<SerilogLoggingAdapter>();

        public PlayerActor()
        {
            _playerRepo = new Repository<Player>("mongodb://localhost:27017/MatchDatabase");

            Receive<GetAllPlayersRequest>(message => Handle(message));
            Receive<GetPlayerByIdRequest>(message => Handle(message));
            Receive<CreatePlayerRequest>(message => Handle(message));
            Receive<RemovePlayerRequest>(message => Handle(message));
            Receive<EditPlayerRequest>(message => Handle(message));
        }

        private void Handle(GetAllPlayersRequest request)
        {
            try
            {
                var notDeleted = new GetPlayersNotDeletedSpecifications();

                var players = _playerRepo.Find(notDeleted).Select(x => new GetPlayerItem(x.Id, x.FirstName, x.LastName, x.NickName, x.Age, x.Sex, x.IsDeleted)).OrderBy(x => x.FirstName);

                var response = new GetAllPlayersResponse(players);

                Sender.Tell(response);
                //Sender.Tell(new GetAllPlayersResponse(Enumerable.Empty<GetPlayerItem>()));

                _logger.Info("Get All Players");
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get all Players: {0}", ex.Message);
                throw;
            }
        }

        private void Handle(GetPlayerByIdRequest request)
        {
            try
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Player, GetPlayerItem>();
                });

                var mapper = config.CreateMapper();

                var player = mapper.Map<GetPlayerItem>(_playerRepo.Get(request.Id));

                var response = new GetPlayerByIdResponse(player);
                Sender.Tell(response);

                _logger.Info("Get Player by Id: {0}", request.Id);
            }
            catch (Exception ex)
            {
                _logger.Error("Could't get Player by id: {0} : {1}", request.Id, ex.Message);
                throw;
            }
        }

        private void Handle(CreatePlayerRequest request)
        {
            //throw new Exception();
            //Thread.Sleep(TimeSpan.FromSeconds(30)); //- test aby sprawdzić, czy zrobi się dziecko aktora
            //tzn. że wiele osob będzie mogło w tym samym czasie korzystać z aplikacji

            try
            {
                if (CheckInputs(firstName: request.FirstName, lastName: request.LastName, nickName: request.NickName,
                                age: request.Age, sex: request.Sex))
                {
                    _playerRepo.Insert(new Player
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        NickName = request.NickName,
                        Age = request.Age,
                        Sex = request.Sex,
                        IsDeleted = false
                    });

                    var response = new CreatePlayerResponse(true);
                    Sender.Tell(response);

                    _logger.Info("Create Player successfull: {0} {1}", request.FirstName, request.LastName);
                }
                else
                {
                    var response = new CreatePlayerResponse(false);
                    Sender.Tell(response);
                    _logger.Error("Couldn't create Player: {0} {1}: Are fields are required", request.FirstName, request.LastName);
                }
            }
            catch (Exception ex)
            {
                var response = new CreatePlayerResponse(false);
                Sender.Tell(response);
                _logger.Error("Couldn't create Player: {0} {1}: {2}", request.FirstName, request.LastName, ex.Message);
            }
        }

        private void Handle(RemovePlayerRequest request)
        {
            try
            {
                //_playerRepo.Delete(request.Id);

                var player = _playerRepo.Get(request.Id);

                player.IsDeleted = true;

                _playerRepo.Replace(player);

                var response = new RemovePlayerResponse(true);
                Sender.Tell(response);

                _logger.Info("Remove Player successfull: {0} {1}", player.FirstName, player.LastName);
            }
            catch (Exception ex)
            {
                var response = new RemovePlayerResponse(false);
                Sender.Tell(response);
                _logger.Error("Couldn't remove Player by id: {0}: {1}", request.Id, ex.Message);
            }
        }

        private void Handle(EditPlayerRequest request)
        {
            try
            {
                var player = _playerRepo.Get(request.Id);

                //var mapper = config.CreateMapper();

                //var player = mapper.Map<GetPlayerItem>(_playerRepo.Get(request.Player.Id));

                player.Id = player.Id;
                player.FirstName = request.FirstName;
                player.LastName = request.LastName;
                player.NickName = request.NickName;
                player.Age = request.Age;
                player.Sex = request.Sex;

                _playerRepo.Replace(player);
                var response = new EditPlayerResponse(true);
                Sender.Tell(response);

                _logger.Info("Edit Player successfull: {0} {1}", player.FirstName, player.LastName);
            }
            catch (Exception ex)
            {
                var response = new EditPlayerResponse(false);
                Sender.Tell(response);

                _logger.Error("Couldn't Player by id: {0}: {1}", request.Id, ex.Message);
            }
        }

        #region Data

        private static bool CheckInputs(string firstName, string lastName, string nickName, int age, Sex sex)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(nickName)
                || String.IsNullOrEmpty((age).ToString()) || String.IsNullOrEmpty((sex).ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
