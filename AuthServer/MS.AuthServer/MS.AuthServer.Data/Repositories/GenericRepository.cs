using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MS.AuthServer.Core.Repositories;

namespace MS.AuthServer.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;
    public GenericRepository(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        var entities = dbSet.Where(predicate);
        return entities;
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }


    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);

        if (entity is not null)
        {
            dbSet.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }
    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public T Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        var newEntity = dbSet.Update(entity);
        return newEntity.Entity;
    }

}
