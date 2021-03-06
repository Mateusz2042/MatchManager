﻿using MatchManager.Enums;
using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Messages.Team.TeamResponse
{
    public class GetAllTeamsResponse
    {
        public ICollection<GetTeamItem> Teams { get; }

        public GetAllTeamsResponse(IEnumerable<GetTeamItem> teams)
        {
            Teams = teams.ToList();
        }
    }

    public class GetTeamItem
    {
        public string Id { get; set; }
        public string NameTeam { get; set; }
        public MatchManager.Models.Player FirstMember { get; set; }
        public MatchManager.Models.Player SecondMember { get; set; }
        public bool IsDeleted { get; set; }

        public GetTeamItem(string id, string nameTeam, MatchManager.Models.Player firstMember, MatchManager.Models.Player secondMember, bool isDeleted)
        {
            Id = id;
            NameTeam = nameTeam;
            FirstMember = firstMember;
            SecondMember = secondMember;
            IsDeleted = isDeleted;
        }
    }
}
