using MatchManager.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AkkaService.Specifications.MatchSpecifications
{
    public class GetMatchesNotDeletedSpecifications: Specification<Match>
    {
        public Match Match { get; set; }

        public GetMatchesNotDeletedSpecifications()
        {

        }

        public override Expression<Func<Match, bool>> ToExpression()
        {
            return match => !match.IsDeleted;
        }
    }
}
