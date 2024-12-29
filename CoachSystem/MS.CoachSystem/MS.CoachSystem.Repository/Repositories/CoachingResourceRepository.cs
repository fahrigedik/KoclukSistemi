using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
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

    public async Task<List<CoachingResourceWithDetails>> GetCoachingResourceWithDetails(List<CoachingResource> coachingResources)
    {
        var coachingResourceWithDetails = context.CoachingResources
            .Include(x => x.ResourceTypeNavigation)
            .Include(x => x.ResourceToTags)
            .ThenInclude(x => x.ResourceTag)
            .Where(x => coachingResources.Contains(x))
            .Select(x => new CoachingResourceWithDetails
            {
                Id = x.Id,
                CoachID = x.CoachID,
                StudentID = x.StudentID,
                Title = x.Title,
                Description = x.Description,
                URL = x.URL,
                ResourceTypeId = x.ResourceTypeId,
                ResourceTypeName = x.ResourceTypeNavigation.TypeName,
                Tags = x.ResourceToTags.Select(x => x.ResourceTag.TagName).ToList()
            }).ToList();

        return coachingResourceWithDetails;
    }

}

