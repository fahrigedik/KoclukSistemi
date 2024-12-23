using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class ResourceToTag : BaseEntity
{
    public int ResourceID { get; set; }
    public int ResourceTagID { get; set; }

    public virtual CoachingResource Resource { get; set; } // Navigation property
    public virtual ResourceTag ResourceTag { get; set; } // Navigation property

}
