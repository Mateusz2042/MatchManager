using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AkkaService.Specifications.PlayerSpecifications
{
    public class GetPlayersNotDeletedSpecifications: Specification<Player>
    {
        public Player Player { get; set; }

        public GetPlayersNotDeletedSpecifications()
        {

        }

        public override Expression<Func<Player, bool>> ToExpression()
        {
            return player => !player.IsDeleted;
        }
    }
}
