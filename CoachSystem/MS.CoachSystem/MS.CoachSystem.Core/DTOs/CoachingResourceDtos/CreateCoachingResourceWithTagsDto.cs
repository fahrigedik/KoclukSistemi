using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MS.CoachSystem.Core.DTOs.CoachingResourceDtos
{
    public class CreateCoachingResourceWithTagsDto
    {
        public string CoachID { get; set; }
        public string StudentID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public int ResourceTypeId { get; set; }

        public List<SelectListItem> ResourceTypes { get; set; }

        public List<SelectListItem> Tags { get; set; }

        public List<string> SelectedTags { get; set; }

    }
}
