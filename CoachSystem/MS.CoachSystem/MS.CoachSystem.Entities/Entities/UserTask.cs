using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class UserTask : BaseEntity, IHasCreationAndModificationDates
{
    public string StudentID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string CoachID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsWorking { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime? ModificationDate { get; set; }
}