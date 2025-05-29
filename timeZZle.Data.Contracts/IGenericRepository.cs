using System.Linq.Expressions;
using timeZZle.Domain.Utils;

namespace timeZZle.Data.Contracts;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task Add(T entity);

    void Remove(T entity);

    Task<int> SaveChangesAsync();

    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? searchExpression, params string[] includes);

    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? searchExpression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        params string[] includes);
}