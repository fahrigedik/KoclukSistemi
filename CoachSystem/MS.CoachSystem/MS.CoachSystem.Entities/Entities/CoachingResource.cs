using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class CoachingResource : BaseEntity, IHasCreationAndModificationDates
{
    public string CoachID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }

    public int ResourceTypeId { get; set; }
    public virtual ResourceType ResourceTypeNavigation { get; set; } // Navigation property
    public virtual ICollection<ResourceToTag> ResourceToTags { get; set; } = new List<ResourceToTag>(); // Navigation property
}

