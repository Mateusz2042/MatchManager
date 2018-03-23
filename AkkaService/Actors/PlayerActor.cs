using Akka.Actor;
using AkkaService.Messages.Player.PlayerRequest;
using AkkaService.Messages.Player.PlayerResponse;
using AkkaService.Specifications.PlayerSpecifications;
using AutoMapper;
using DotNETCore.Repository.Mongo;
using MatchManager.Enums;
using MatchManager.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AkkaService.Actors
{
    public class PlayerActor: ReceiveActor // utworzenie PlayerActora
    {
        private readonly IRepository<Player> _playerRepo;

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

                var players = _playerRepo.Find(notDeleted).Select(x => new GetPlayerItem(x.Id, x.FirstName, x.LastName, x.NickName, x.Age, x.Sex, x.IsDeleted));

                var response = new GetAllPlayersResponse(players);

                Sender.Tell(response);
                //Sender.Tell(new GetAllPlayersResponse(Enumerable.Empty<GetPlayerItem>()));
            }
            catch (Exception)
            {

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
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Handle(CreatePlayerRequest request)
        {
            //throw new Exception();
            //Thread.Sleep(TimeSpan.FromSeconds(30)); - test aby sprawdzić, czy zrobi się dziecko aktora
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
                }
                else
                {
                    var response = new CreatePlayerResponse(false);
                    Sender.Tell(response);
                }
            }
            catch (Exception)
            {
                var response = new CreatePlayerResponse(false);
                Sender.Tell(response);
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
            }
            catch (Exception)
            {
                var response = new RemovePlayerResponse(false);
                Sender.Tell(response);
            }
        }

        private void Handle(EditPlayerRequest request)
        {
            try
            {
                var player = _playerRepo.Get(request.Id);

                //var mapper = config.CreateMapper();

                //var player = mapper.Map<GetPlayerItem>(_playerRepo.Get(request.Player.Id));

                player.FirstName = request.FirstName;
                player.LastName = request.LastName;
                player.NickName = request.NickName;
                player.Age = request.Age;
                player.Sex = request.Sex;

                _playerRepo.Replace(player);
                var response = new EditPlayerResponse(true);
                Sender.Tell(response);
            }
            catch (Exception)
            {
                var response = new EditPlayerResponse(false);
                Sender.Tell(response);
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
