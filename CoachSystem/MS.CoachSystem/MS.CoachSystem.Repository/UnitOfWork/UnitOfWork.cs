using MS.CoachSystem.Core.UnitOfWork;

namespace MS.CoachSystem.Repository.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public void Rollback()
    {
        context.Database.RollbackTransaction();
    }

    public async Task RollbackAsync()
    {
        await context.Database.RollbackTransactionAsync();
    }
}