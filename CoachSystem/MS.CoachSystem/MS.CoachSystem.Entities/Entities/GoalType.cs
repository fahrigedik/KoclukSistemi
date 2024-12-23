using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class GoalType : BaseEntity
{
    public string TypeName { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>(); // Navigation property
}
