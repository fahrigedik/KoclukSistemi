using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class ResourceType : BaseEntity
{
    public string TypeName { get; set; }
    public virtual ICollection<CoachingResource> CoachingResources { get; set; } = new List<CoachingResource>(); // Navigation property
}

