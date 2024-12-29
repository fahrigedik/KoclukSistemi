namespace MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
public class CoachingResourceWithDetails
{
    public int Id { get; set; }
    public string CoachID { get; set; }
    public string StudentID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public int ResourceTypeId { get; set; }
    public string ResourceTypeName { get; set; }
    public List<string> Tags { get; set; }
}
