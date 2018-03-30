using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MatchAppUI.Models;
using MatchManager.Models;
using System.Net.Http;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;

namespace MatchAppUI.Controllers
{
    public class PlayerController : Controller
    {
        public class DeserializePlayers
        {
            public IEnumerable<Player> Players { get; set; }
        }

        public IActionResult GetAllPlayers()
        {
            return View(deserialize().Players);
        }

        public IActionResult CreatePlayer()
        {
            return View();
        }

        public IActionResult EditPlayer(string id)
        {
            //ViewBag.PlayerId = id;

            return View(deserialize(id));
        }

        public IActionResult DeletePlayer(string id)
        {
            ViewBag.PlayerId = id;

            return View();
        }
        
        public IActionResult DetailsPlayer(string id)
        {
            ViewBag.PlayerId = id;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region MyRegion

        public DeserializePlayers deserialize()
        {
            var url = string.Format("http://localhost:53766/api/Player/");

            // Syncronious Consumption
            var syncClient = new WebClient();

            var content = syncClient.DownloadString(url);

            DeserializePlayers players = JsonConvert.DeserializeObject<DeserializePlayers>(content);

            return players;
        }

        public Player deserialize(string id)
        {
            var url = string.Format("http://localhost:53766/api/Player/");

            // Syncronious Consumption
            var syncClient = new WebClient();

            var content = syncClient.DownloadString(url);

            DeserializePlayers players = JsonConvert.DeserializeObject<DeserializePlayers>(content);

            var player = players.Players.Where(x => x.Id == id).FirstOrDefault();

            return player;
        }

        #endregion
    }
}
