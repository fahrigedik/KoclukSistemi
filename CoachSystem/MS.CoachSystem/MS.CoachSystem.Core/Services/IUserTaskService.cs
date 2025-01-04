using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;

public interface IUserTaskService : IGenericService<UserTask, UserTaskDto>
{
    Task<GenericResponse<List<UserTask>>> GetUserTaskByStudentIdAsync(UserTaskRequestDto userTaskRequestDto);
    Task<GenericResponse<UserTaskDto>> MarkTaskAsCompleted(UserTaskDto userTask);
    Task<GenericResponse<UserTaskDto>> MarkTaskAsWorking(UserTaskDto userTask);
    Task<int> GetCountByCoachId(string coachId);
}


