using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;
public interface ICoachingResourceService : IGenericService<CoachingResource, CoachingResourceDto>
{
    Task<GenericResponse<List<CoachingResourceWithDetails>>> GetAllCoachingResourceWithDetailByStudentIdAsync(CoachingResourceRequestDto coachingResourceRequest);

    Task<GenericResponse<CoachingResourceWithDetails>> GetCoachingResourceWithDetailById(int id);

    Task<GenericResponse<CreateCoachingResourceWithTagsDto>> AddResourceWithTagsAsync(CreateCoachingResourceWithTagsDto resourceDto, List<int> tagIds);

    Task<GenericResponse<CreateCoachingResourceWithTagsDto>> UpdateResourceWithTagsAsync(CreateCoachingResourceWithTagsDto resourceDto, List<int> tagIds, int resourceId);

    Task DeleteAsync(int id);
}
