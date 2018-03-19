using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Application.Specifications
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not(Specification<T> specification)
        {
            return new NotSpecification<T>(this, specification);
        }
    }

    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _firstSpecification;
        private readonly Specification<T> _secondSpecification;

        public AndSpecification(Specification<T> firstSpecification, Specification<T> secondSpecification)
        {
            _firstSpecification = firstSpecification;
            _secondSpecification = secondSpecification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> firstExpression = _firstSpecification.ToExpression();
            Expression<Func<T, bool>> secondExpression = _secondSpecification.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(firstExpression.Body, secondExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, firstExpression.Parameters.Single());
        }
    }

    internal class OrSpecification<T> : Specification<T>
    {
        private Specification<T> _firstSpecification;
        private Specification<T> _secondSpecification;

        public OrSpecification(Specification<T> firstSpecification, Specification<T> secondSpecification)
        {
            _firstSpecification = firstSpecification;
            _secondSpecification = secondSpecification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> firstExpression = _firstSpecification.ToExpression();
            Expression<Func<T, bool>> secondExpression = _secondSpecification.ToExpression();

            BinaryExpression orExpression = Expression.Or(firstExpression.Body, secondExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, firstExpression.Parameters.Single());
        }
    }
    internal class NotSpecification<T> : Specification<T>
    {
        private Specification<T> _firstSpecification;
        private Specification<T> _secondSpecification;

        public NotSpecification(Specification<T> firstSpecification, Specification<T> secondSpecification)
        {
            _firstSpecification = firstSpecification;
            _secondSpecification = secondSpecification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> firstExpression = _firstSpecification.ToExpression();
            Expression<Func<T, bool>> secondExpression = _secondSpecification.ToExpression();

            UnaryExpression notExpression = Expression.Not(firstExpression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, firstExpression.Parameters.Single());
        }
    }

}
