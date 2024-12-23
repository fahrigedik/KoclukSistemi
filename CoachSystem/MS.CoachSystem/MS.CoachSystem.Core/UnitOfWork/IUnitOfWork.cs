namespace MS.CoachSystem.Core.UnitOfWork;
public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();

    public int SaveChanges();

    public void Rollback();

    public Task RollbackAsync();
}

