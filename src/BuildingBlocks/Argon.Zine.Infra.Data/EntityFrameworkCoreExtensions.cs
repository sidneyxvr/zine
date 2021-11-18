using System.Linq.Expressions;

namespace Argon.Zine.Infra.Data;

public static class EntityFrameworkCoreExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source, 
        bool evaluation, 
        Expression<Func<T, bool>> predicate)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return evaluation
            ? source.Where(predicate)
            : source;
    }

    public static IQueryable<T> OrderByIf<T>(
        this IQueryable<T> source, 
        bool evaluation, 
        Expression<Func<T, bool>> predicate)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return evaluation
            ? source.OrderBy(predicate)
            : source;
    }

    public static IQueryable<T> OrderByDescendingIf<T>(
        this IQueryable<T> source, 
        bool evaluation, 
        Expression<Func<T, bool>> predicate)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return evaluation
            ? source.OrderByDescending(predicate)
            : source;
    }
}