using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.GoalDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;
public interface IGoalService : IGenericService<Goal, GoalDto>
{
    Task<GenericResponse<List<GoalWithTypeNamesDto>>> GetAllGoalWithTypeByStudentId(GoalRequestDto goalRequest);

}

