using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;

public class GoalTypeService : GenericService<GoalType, GoalTypeDto> , IGoalTypeService
{
    private readonly IGenericRepository<GoalType> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public GoalTypeService(IGenericRepository<GoalType> _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }
}
