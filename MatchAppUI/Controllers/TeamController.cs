using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MatchManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MatchAppUI.Controllers
{
    public class TeamController : Controller
    {
        public class DeserializeTeams
        {
            public IEnumerable<Team> Teams { get; set; }
        }

        public IActionResult GetAllTeams()
        {
            return View(deserialize().Teams);
        }

        public IActionResult Index()
        {
            return View();
        }

        #region MyRegion

        public DeserializeTeams deserialize()
        {
            var url = string.Format("http://localhost:53766/api/Match/");

            // Syncronious Consumption
            var syncClient = new WebClient();

            var content = syncClient.DownloadString(url);

            DeserializeTeams teams = JsonConvert.DeserializeObject<DeserializeTeams>(content);

            return teams;
        }

        #endregion
    }
}