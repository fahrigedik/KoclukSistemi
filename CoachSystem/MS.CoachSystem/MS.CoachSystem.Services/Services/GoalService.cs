using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.Core.UnitOfWork;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Service.Services;
public class GoalService : GenericService<Goal, GoalDto>, IGoalService
{
    private readonly IGenericRepository<Goal> repository;
    private readonly IGoalTypeRepository goalTypeRepository;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public GoalService(IGenericRepository<Goal> _repository, IGoalTypeRepository goalTypeRepository, IMapper _mapper, IUnitOfWork _unitOfWork) : base(_repository, _mapper, _unitOfWork)
    {
        repository = _repository;
        this.goalTypeRepository = goalTypeRepository;
        mapper = _mapper;
        unitOfWork = _unitOfWork;
    }

    public async Task<GenericResponse<List<Goal>>> GetAllGoalWithTypeByStudentId(GoalRequestDto goalRequest)
    {
        var goals = await repository
            .Where(x => x.CoachID == goalRequest.CoachID && x.StudentID == goalRequest.StudentID).ToListAsync();
        foreach (var goal in goals)
        {
            var goalType = await goalTypeRepository.GetByIdAsync(goal.GoalTypeId);
            goal.GoalType = goalType;
        }
        return GenericResponse<List<Goal>>.Success(goals, HttpStatusCode.OK);
    }
}
