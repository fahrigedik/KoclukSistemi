using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class ResourceTypeRepository : GenericRepository<ResourceType>, IResourceTypeRepository
{
    private readonly AppDbContext context;
    public ResourceTypeRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
