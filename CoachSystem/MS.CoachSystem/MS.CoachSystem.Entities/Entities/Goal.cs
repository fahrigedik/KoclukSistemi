using System.Dynamic;
using MS.CoachSystem.Entity.Base;

namespace MS.CoachSystem.Entity.Entities;

public class Goal : BaseEntity, IHasCreationAndModificationDates
{
    public string StudentID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string CoachID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string GoalTitle { get; set; } // Hedefin başlığı
    public string GoalDescription { get; set; } // Hedefin açıklaması için daha açıklayıcı bir isim
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int? GoalTypeId { get; set; } 
    public virtual GoalType? GoalType { get; set; } // Navigation property
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsWorking { get; set; } = false;
}

