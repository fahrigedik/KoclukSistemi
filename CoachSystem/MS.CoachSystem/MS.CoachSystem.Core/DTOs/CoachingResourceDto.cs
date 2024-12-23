namespace MS.CoachSystem.Core.DTOs;

public class CoachingResourceDto
{
    public string CoachID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ResourceType { get; set; }
    public string URL { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
