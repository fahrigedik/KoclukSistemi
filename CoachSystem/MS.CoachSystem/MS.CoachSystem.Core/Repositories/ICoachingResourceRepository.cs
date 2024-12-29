using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Repositories;

public interface ICoachingResourceRepository : IGenericRepository<CoachingResource>
{
    Task<List<CoachingResourceWithDetails>> GetCoachingResourceWithDetails(List<CoachingResource> coachingResources);
}

