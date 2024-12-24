using System.Linq.Expressions;

namespace MS.CoachSystem.Core.Repositories;

public interface IGenericRepository<T> where T: class
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    void Delete(T entity);
    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
}

