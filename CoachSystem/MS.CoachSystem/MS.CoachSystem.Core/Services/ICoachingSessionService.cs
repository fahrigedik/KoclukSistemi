using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Core.DTOs.CoachingSessionDtos;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Services;
public interface ICoachingSessionService : IGenericService<CoachingSession, CoachingSessionDto>
{
    Task<GenericResponse<List<CoachingSession>>> GetCoachingSessionByStudentIdAsync(CoachingSessionRequestDto requestDto);
}

