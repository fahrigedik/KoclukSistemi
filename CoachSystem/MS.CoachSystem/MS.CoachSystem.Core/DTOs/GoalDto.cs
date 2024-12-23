using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Core.DTOs;
public class GoalDto
{
    public string StudentID { get; set; }
    public string CoachID { get; set; }
    public string GoalTitle { get; set; }
    public string GoalDescription { get; set; }
    public string Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int GoalTypeId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
