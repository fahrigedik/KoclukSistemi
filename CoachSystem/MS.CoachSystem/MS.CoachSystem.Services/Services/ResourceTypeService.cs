using AutoMapper;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class ResourceTypeService : GenericService<ResourceType, ResourceTypeDto>, IResourceTypeService
{
    private readonly IGenericRepository<ResourceType> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public ResourceTypeService(IGenericRepository<ResourceType> _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }
}