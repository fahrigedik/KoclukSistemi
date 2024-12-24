using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class CoachingResourceRepository : GenericRepository<CoachingResource>, ICoachingResourceRepository
{
    private readonly AppDbContext context;
    public CoachingResourceRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}

