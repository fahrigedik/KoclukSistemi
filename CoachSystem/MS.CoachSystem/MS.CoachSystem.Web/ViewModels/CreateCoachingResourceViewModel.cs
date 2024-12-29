using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MS.CoachSystem.Web.ViewModels
{
    public class CreateCoachingResourceViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public int ResourceTypeId { get; set; }

        public List<SelectListItem>? ResourceTypes { get; set; }

        public List<SelectListItem>? Tags { get; set; }

        public List<string> SelectedTags { get; set; }
    }
}
