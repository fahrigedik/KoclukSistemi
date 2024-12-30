using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.DTOs.GoalDtos;
public class GoalWithTypeNamesDto
{
    public int Id { get; set; }
    public string StudentID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string CoachID { get; set; } // AuthServer'daki Users tablosuna foreign key
    public string GoalTitle { get; set; } // Hedefin başlığı
    public string GoalDescription { get; set; } // Hedefin açıklaması için daha açıklayıcı bir isim
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int GoalTypeId { get; set; } // GoalType Id
    public string GoalTypeName { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}
