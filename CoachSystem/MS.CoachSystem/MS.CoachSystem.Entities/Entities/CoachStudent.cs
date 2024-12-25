using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;
public class CoachStudent : BaseEntity
{
    public string CoachId { get; set; }
    public string StudentId { get; set; }
}