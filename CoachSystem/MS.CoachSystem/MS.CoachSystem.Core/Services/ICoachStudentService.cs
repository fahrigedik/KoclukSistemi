using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;
public interface ICoachStudentService : IGenericService<CoachStudent, CoachStudentDto>
{
    Task<GenericResponse<List<string>>> GetStudentIdsByCoachIdAsync(string coachId);

    Task<GenericResponse<string>> GetEntityIdByCoachIdAndStudentId(string coachId, string studentId);

    Task<GenericResponse<List<string>>> GetCoachIdByStudentIdAsync(string studentId);
}
