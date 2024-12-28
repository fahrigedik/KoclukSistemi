using Microsoft.AspNetCore.Mvc.Rendering;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Web.ViewModels;
public class UpdateGoalViewModel
{
    public List<SelectListItem>? GoalTypes { get; set; }
    public Goal Goal { get; set; }
}
