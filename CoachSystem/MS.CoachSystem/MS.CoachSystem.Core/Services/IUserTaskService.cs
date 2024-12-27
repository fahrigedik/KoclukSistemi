using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.UserTaskDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;

public interface IUserTaskService : IGenericService<UserTask, UserTaskDto>
{
    Task<GenericResponse<List<UserTaskDto>>> GetUserTaskByStudentIdAsync(UserTaskRequestDto userTaskRequestDto);
}


