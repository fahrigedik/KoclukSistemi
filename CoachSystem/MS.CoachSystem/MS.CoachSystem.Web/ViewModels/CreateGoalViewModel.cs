using Microsoft.AspNetCore.Mvc.Rendering;

namespace MS.CoachSystem.Web.ViewModels
{
    public class CreateGoalViewModel
    {
        public string CoachID { get; set; }
        public string StudentID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int GoalTypeId { get; set; }
        public List<SelectListItem>? GoalTypes { get; set; }
    }
}
