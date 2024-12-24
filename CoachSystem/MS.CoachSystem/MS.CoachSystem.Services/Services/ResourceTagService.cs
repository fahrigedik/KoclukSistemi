using AutoMapper;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class ResourceTagService : GenericService<ResourceTag, ResourceTagDto>, IResourceTagService
{
    private readonly IGenericRepository<ResourceTag> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public ResourceTagService(IGenericRepository<ResourceTag> _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }
}

