using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.Repositories;

namespace MS.CoachSystem.Repository.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext context;
    private readonly DbSet<T> dbSet;
    public GenericRepository(AppDbContext _context)
    {
        context = _context;
        dbSet = context.Set<T>();
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);

        if (entity is not null)
        {
            context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var entities = await dbSet.ToListAsync();

        return entities;
    }

    public async Task<T> AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        var newEntity = dbSet.Update(entity).Entity;
        return newEntity;
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        var entities = dbSet.Where(predicate);
        return entities;
    }
}

