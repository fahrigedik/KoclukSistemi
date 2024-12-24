using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class ResourceToTagRepository : GenericRepository<ResourceToTag>, IResourceToTagRepository
{
    private readonly AppDbContext context;
    public ResourceToTagRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
