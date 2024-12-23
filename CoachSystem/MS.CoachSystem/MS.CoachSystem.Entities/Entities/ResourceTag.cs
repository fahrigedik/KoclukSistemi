using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class ResourceTag : BaseEntity
{
    public string TagName { get; set; }

    public virtual ICollection<ResourceToTag> ResourceToTags { get; set; } = new List<ResourceToTag>(); // Navigation property
}
