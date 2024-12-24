using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class UserTaskRepository : GenericRepository<UserTask>, IUserTaskRepository
{
    private readonly AppDbContext context;
    public UserTaskRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
