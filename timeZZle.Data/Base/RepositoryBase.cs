using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using timeZZle.Data.Context;

namespace timeZZle.Data.Base;

public class RepositoryBase<T> where T : class
{
    protected readonly AppDbContext Context;

    protected RepositoryBase(AppDbContext context)
    {
        Context = context;
    }

    public async Task Add(T entity)
    {
        await Context.AddAsync(entity);
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? searchExpression, params string[] includes)
    {
        return await GetAll(searchExpression, null, includes);
    }

    public async Task<IEnumerable<T>> GetAll(
        Expression<Func<T, bool>>? searchExpression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        params string[] includes)
    {
        IQueryable<T> query = Context.Set<T>();

        if (searchExpression != null)
        {
            query = query.Where(searchExpression);
        }

        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToArrayAsync();
        }

        return await query.ToArrayAsync();

    }
}