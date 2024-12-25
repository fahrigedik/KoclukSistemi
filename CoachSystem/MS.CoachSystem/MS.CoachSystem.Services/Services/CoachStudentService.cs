using AutoMapper;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class CoachStudentService : GenericService<CoachStudent, CoachStudentDto>, ICoachStudentService
{
    private readonly IGenericRepository<CoachStudent> repository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public CoachStudentService(IGenericRepository<CoachStudent> _repository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {

    }
}
