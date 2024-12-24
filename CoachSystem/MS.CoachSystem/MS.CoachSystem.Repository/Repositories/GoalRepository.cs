using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class GoalRepository : GenericRepository<Goal>, IGoalRepository
{
    private readonly AppDbContext context;
    public GoalRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
