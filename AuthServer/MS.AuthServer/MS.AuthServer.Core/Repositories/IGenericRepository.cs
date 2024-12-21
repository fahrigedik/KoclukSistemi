using System.Linq.Expressions;

namespace MS.AuthServer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id);

    Task<IEnumerable<T>> GetAllAsync();

    IQueryable<T> Where(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);

    void Remove(T entity);

    T Update(T entity);
}
