using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MatchManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MatchAppUI.Controllers
{
    public class MatchController : Controller
    {
        public class DeserializeMatches
        {
            public IEnumerable<Match> Matches { get; set; }
        }

        public IActionResult GetAllMatches()
        {
            return View(deserialize().Matches);
        }

        public IActionResult CreateMatch()
        {
            return View();
        }

        public IActionResult CreateRandomMatch()
        {
            return View();
        }

        public IActionResult EditMatch(string id)
        {
            return View(deserialize(id));
        }

        public IActionResult DeleteMatch(string id)
        {
            ViewBag.MatchId = id;

            return View();
        }

        public IActionResult DetailsMatch(string id)
        {
            ViewBag.MatchId = id;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        #region MyRegion

        public DeserializeMatches deserialize()
        {
            var url = string.Format("http://localhost:53766/api/Match/");

            // Syncronious Consumption
            var syncClient = new WebClient();

            var content = syncClient.DownloadString(url);

            DeserializeMatches matches = JsonConvert.DeserializeObject<DeserializeMatches>(content);

            return matches;
        }

        public Match deserialize(string id)
        {
            var url = string.Format("http://localhost:53766/api/Match/");

            // Syncronious Consumption
            var syncClient = new WebClient();

            var content = syncClient.DownloadString(url);

            DeserializeMatches matches = JsonConvert.DeserializeObject<DeserializeMatches>(content);

            var match = matches.Matches.Where(x => x.Id == id).FirstOrDefault();

            return match;
        }

        #endregion
    }
}