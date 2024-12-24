using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class GoalTypeRepository : GenericRepository<GoalType>, IGoalTypeRepository
{
    private readonly AppDbContext context;
    public GoalTypeRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
