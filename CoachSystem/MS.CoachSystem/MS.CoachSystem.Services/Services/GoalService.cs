using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Core.Enum;
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

    public async Task<GenericResponse<List<GoalWithTypeNamesDto>>> GetAllGoalWithTypeByStudentId(GoalRequestDto goalRequest)
    {
        var goals = await repository
            .Where(x => x.CoachID == goalRequest.CoachID && x.StudentID == goalRequest.StudentID)
            .Include(x => x.GoalType)
            .ToListAsync();

        var goalDtos = goals.Select(goal => new GoalWithTypeNamesDto
        {
            Id = goal.Id,
            StudentID = goal.StudentID,
            CoachID = goal.CoachID,
            GoalTitle = goal.GoalTitle,
            GoalDescription = goal.GoalDescription,
            Status = goal.Status,
            DueDate = goal.DueDate,
            CompletedDate = goal.CompletedDate,
            IsCompleted = goal.IsCompleted,
            IsWorking = goal.IsWorking,
            GoalTypeId = goal.GoalTypeId,
            GoalTypeName = goal.GoalType.TypeName,
            CreationDate = goal.CreationDate,
            ModificationDate = goal.ModificationDate
        }).ToList();

        return GenericResponse<List<GoalWithTypeNamesDto>>.Success(goalDtos, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<GoalDto>> MarkGoalAsCompletedAsync(GoalDto goalDto)
    {
        if (goalDto.IsCompleted == false)
        {
            goalDto.Status = Status.Tamamlandı.ToString();
            goalDto.CompletedDate = DateTime.Now;
            goalDto.IsCompleted = true;
            goalDto.IsWorking = false;
        }
        else
        {
            goalDto.Status = Status.Tamamlanmadı.ToString();
            goalDto.CompletedDate = null;
            goalDto.IsCompleted = false;
        }
        return GenericResponse<GoalDto>.Success(goalDto, HttpStatusCode.OK);
    }

    public async Task<GenericResponse<GoalDto>> MarkGoalAsWorkingAsync(GoalDto goalDto)
    {
        if (goalDto.IsWorking == false)
        {
            goalDto.Status = Status.Çalışılıyor.ToString();
            goalDto.IsWorking = true;
            goalDto.CompletedDate = null;

            goalDto.IsCompleted = false;
            goalDto.CompletedDate = null;
        }
        else
        {
            goalDto.Status = Status.Tamamlanmadı.ToString();
            goalDto.IsWorking = false;
        }

        return GenericResponse<GoalDto>.Success(goalDto, HttpStatusCode.OK);
    }
}
