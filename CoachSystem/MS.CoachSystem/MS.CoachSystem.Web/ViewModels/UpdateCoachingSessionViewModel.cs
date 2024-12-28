using System.ComponentModel.DataAnnotations;

namespace MS.CoachSystem.Web.ViewModels
{
    public class UpdateCoachingSessionViewModel
    {
        public int Id { get; set; }

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
