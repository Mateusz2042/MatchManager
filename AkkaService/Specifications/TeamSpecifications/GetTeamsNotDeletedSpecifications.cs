using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AkkaService.Specifications.TeamSpecifications
{
    public class GetTeamsNotDeletedSpecifications: Specification<Team>
    {
        public Team Team { get; set; }

        public GetTeamsNotDeletedSpecifications()
        {

        }

        public override Expression<Func<Team, bool>> ToExpression()
        {
            return team => !team.IsDeleted;
        }
    }
}
