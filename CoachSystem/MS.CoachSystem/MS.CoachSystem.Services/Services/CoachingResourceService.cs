using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;
using MS.CoachSystem.Repository.Repositories;

namespace MS.CoachSystem.Service.Services;
public class CoachingResourceService : GenericService<CoachingResource, CoachingResourceDto>, ICoachingResourceService
{
    private readonly IGenericRepository<CoachingResource> repository;
    private readonly ICoachingResourceRepository coachingResourceRepository;
    private readonly IResourceToTagRepository _resourceToTagRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public CoachingResourceService(IGenericRepository<CoachingResource> _repository, ICoachingResourceRepository _coachingResourceRepository, IResourceToTagRepository _resourceToTagRepository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
        coachingResourceRepository = _coachingResourceRepository;
        this._resourceToTagRepository = _resourceToTagRepository;
    }

    public async Task<GenericResponse<List<CoachingResourceWithDetails>>> GetAllCoachingResourceWithDetailByStudentIdAsync(CoachingResourceRequestDto coachingResourceRequest)
    {
        var coachingResources = await repository.Where(x =>
            x.CoachID == coachingResourceRequest.CoachID && x.StudentID == coachingResourceRequest.StudentID).ToListAsync();

        if (coachingResources.Count == 0)
        {
            return GenericResponse<List<CoachingResourceWithDetails>>.Fail("Coaching resources is null", HttpStatusCode.OK, true);
        }

        var coachingResourceWithDetails =
            await coachingResourceRepository.GetCoachingResourceWithDetails(coachingResources);

        return GenericResponse<List<CoachingResourceWithDetails>>.Success(coachingResourceWithDetails, HttpStatusCode.OK);
    }
    public async Task<GenericResponse<CoachingResourceWithDetails>> GetCoachingResourceWithDetailById(int id)
    {
        var resource = await repository.Where(r => r.Id == id)
            .Include(r => r.ResourceTypeNavigation)
            .Include(r => r.ResourceToTags)
            .ThenInclude(rt => rt.ResourceTag)
            .FirstOrDefaultAsync();

        if (resource == null)
        {
            return GenericResponse<CoachingResourceWithDetails>.Fail("Resource not found", HttpStatusCode.NotFound, true);
        }

        var resourceWithDetails = new CoachingResourceWithDetails
        {
            Id = resource.Id,
            CoachID = resource.CoachID,
            StudentID = resource.StudentID,
            Title = resource.Title,
            Description = resource.Description,
            URL = resource.URL,
            CreationDate = resource.CreationDate,
            ModificationDate = resource.ModificationDate,
            ResourceTypeId = resource.ResourceTypeId,
            ResourceTypeName = resource.ResourceTypeNavigation?.TypeName,
            Tags = resource.ResourceToTags.Select(rt => rt.ResourceTag.TagName).ToList()
        };

        return GenericResponse<CoachingResourceWithDetails>.Success(resourceWithDetails, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<CreateCoachingResourceWithTagsDto>> AddResourceWithTagsAsync(CreateCoachingResourceWithTagsDto resourceDto, List<int> tagIds)
    {
        var resource = new CoachingResource
        {
            CoachID = resourceDto.CoachID,
            StudentID = resourceDto.StudentID,
            Title = resourceDto.Title,
            Description = resourceDto.Description,
            URL = resourceDto.URL,
            ResourceTypeId = resourceDto.ResourceTypeId,
            CreationDate = DateTime.Now,
        };

        await repository.AddAsync(resource);

        foreach (var tagId in tagIds)
        {
            var resourceToTag = new ResourceToTag
            {
                ResourceID = resource.Id,
                ResourceTagID = tagId
            };
            await _resourceToTagRepository.AddAsync(resourceToTag);
        }

        return GenericResponse<CreateCoachingResourceWithTagsDto>.Success(resourceDto, HttpStatusCode.Created);
    }

    public async Task<GenericResponse<CreateCoachingResourceWithTagsDto>> UpdateResourceWithTagsAsync(CreateCoachingResourceWithTagsDto resourceDto, List<int> tagIds, int resourceId)
    {
        var resource = await repository.GetByIdAsync(resourceId);
        if (resource == null)
        {
            return GenericResponse<CreateCoachingResourceWithTagsDto>.Fail("Resource not found", HttpStatusCode.NotFound, true);
        }

        resource.Title = resourceDto.Title;
        resource.Description = resourceDto.Description;
        resource.URL = resourceDto.URL;
        resource.ResourceTypeId = resourceDto.ResourceTypeId;
        resource.ModificationDate = DateTime.Now;

        await repository.UpdateAsync(resource);

        var existingTags = await _resourceToTagRepository.Where(rt => rt.ResourceID == resourceId).ToListAsync();
        foreach (var existingTag in existingTags)
        {
            _resourceToTagRepository.Delete(existingTag);
        }

        foreach (var tagId in tagIds)
        {
            var resourceToTag = new ResourceToTag
            {
                ResourceID = resource.Id,
                ResourceTagID = tagId
            };
            await _resourceToTagRepository.AddAsync(resourceToTag);
        }

        return GenericResponse<CreateCoachingResourceWithTagsDto>.Success(resourceDto, HttpStatusCode.OK);
    }

    public async Task DeleteAsync(int id)
    {
        var resource = await repository.GetByIdAsync(id);
        if (resource != null)
        {
            var resourceTags = await _resourceToTagRepository.Where(rt => rt.ResourceID == id).ToListAsync();
            foreach (var resourceTag in resourceTags)
            {
                _resourceToTagRepository.Delete(resourceTag);
            }

            repository.Delete(resource);
            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<int> GetCountByCoachId(string coachId)
    {
        var resources = await repository.Where(x => x.CoachID == coachId).ToListAsync();
        return resources.Count;
    }
}
