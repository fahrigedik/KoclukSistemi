using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.Repositories;
public interface ICoachStudentRepository : IGenericRepository<CoachStudent>
{
    Task<List<string>> GetStudentIdsByCoachIdAsync(string coachId);
}
