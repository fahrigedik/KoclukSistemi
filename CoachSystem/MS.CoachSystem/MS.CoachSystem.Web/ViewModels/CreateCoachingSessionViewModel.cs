using System.ComponentModel.DataAnnotations;

namespace MS.CoachSystem.Web.ViewModels
{
    public class CreateCoachingSessionViewModel
    {

        [Required]
        public string SessionTopic { get; set; }

        public string SessionNotes { get; set; }

        [Required]
        public string SessionLocation { get; set; }

        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public TimeSpan SessionTime { get; set; }

        [Required]
        public string SessionStatus { get; set; }
    }
}
