using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class CoachingSessionRepository : GenericRepository<CoachingSession>, ICoachingSessionRepository
{
    private readonly AppDbContext context;
    public CoachingSessionRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}