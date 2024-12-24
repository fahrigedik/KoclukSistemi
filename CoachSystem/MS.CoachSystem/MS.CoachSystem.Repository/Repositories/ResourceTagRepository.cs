using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class ResourceTagRepository : GenericRepository<ResourceTag>, IResourceTagRepository
{
    private readonly AppDbContext context;
    public ResourceTagRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
    }
}
