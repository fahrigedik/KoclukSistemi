namespace MS.CoachSystem.Core.DTOs;
public class UserTaskDto
{
    public string StudentID { get; set; } 
    public string CoachID { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}

