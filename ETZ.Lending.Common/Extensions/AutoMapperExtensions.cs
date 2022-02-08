using System;
using System.Linq.Expressions;
using AutoMapper;

namespace ETZ.Lending.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination, TMember>(
            this IMappingExpression<TSource, TDestination> expression,
            Expression<Func<TDestination, TMember>> destinationMember)
        {
            expression.ForMember(destinationMember, static opt => opt.Ignore());
            return expression;
        }
    }
}