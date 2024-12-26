using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Core.Repositories;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Repositories;
public class CoachStudentRepository : GenericRepository<CoachStudent>, ICoachStudentRepository
{
    
    private readonly AppDbContext context;
    private readonly DbSet<CoachStudent> dbSet;
    public CoachStudentRepository(AppDbContext _context) : base(_context)
    {
        context = _context;
        dbSet = context.Set<CoachStudent>();
    }

    public async Task<List<string>> GetStudentIdsByCoachIdAsync(string coachId)
    {
        var studentIds = await dbSet.Where(x => x.CoachId == coachId).Select(x => x.StudentId).ToListAsync();
        return studentIds;
    }
}
