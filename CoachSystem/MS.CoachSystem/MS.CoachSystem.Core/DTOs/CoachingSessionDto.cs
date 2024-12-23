
namespace MS.CoachSystem.Core.DTOs;
public class CoachingSessionDto
{
    public string CoachId { get; set; }
    public string StudentId { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan SessionTime { get; set; }
    public TimeSpan? SessionDuration { get; set; }
    public string SessionTopic { get; set; }
    public string SessionNotes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
