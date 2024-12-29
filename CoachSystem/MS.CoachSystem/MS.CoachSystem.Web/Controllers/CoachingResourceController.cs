using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.Services;
using System.Security.Claims;
using MS.CoachSystem.Core.DTOs.CoachingResourceDtos;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MS.CoachSystem.Service.Services;

namespace MS.CoachSystem.Web.Controllers
{
    public class CoachingResourceController : Controller
    {
        private readonly ICoachingResourceService _coachingResourceService;
        private readonly IResourceTypeService _resourceTypeService;
        private readonly IResourceTagService _resourceTagService;

        public CoachingResourceController(ICoachingResourceService coachingResourceService, IResourceTagService resourceTagService, IResourceTypeService resourceTypeService)
        {
            _coachingResourceService = coachingResourceService;
            _resourceTypeService = resourceTypeService;
            _resourceTagService = resourceTagService;
        }
        public async Task<IActionResult> Index()
        {
            var coachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = HttpContext.Session.GetString("ManagedStudentId");
            var requestDto = new CoachingResourceRequestDto()
            {
                CoachID = coachId,
                StudentID = studentId
            };
            var coachingResourceWithDetails = await _coachingResourceService.GetAllCoachingResourceWithDetailByStudentIdAsync(requestDto);
            return View(coachingResourceWithDetails.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var resourceTypes = await _resourceTypeService.GetAllMainEntitiesAsync();
            var tags = await _resourceTagService.GetAllMainEntitiesAsync();

            var createCoachingResourceViewModel = new CreateCoachingResourceViewModel
            {
                ResourceTypes = resourceTypes.Select(rt => new SelectListItem
                {
                    Value = rt.Id.ToString(),
                    Text = rt.TypeName
                }).ToList(),
                Tags = tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.TagName
                }).ToList()
            };

            return View(createCoachingResourceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCoachingResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resourceDto = new CreateCoachingResourceWithTagsDto
                {
                    CoachID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    StudentID = HttpContext.Session.GetString("ManagedStudentId"),
                    Title = model.Title,
                    Description = model.Description,
                    URL = model.URL,
                    ResourceTypeId = model.ResourceTypeId,
                };

                var tagIds = model.SelectedTags.Select(int.Parse).ToList();
                await _coachingResourceService.AddResourceWithTagsAsync(resourceDto, tagIds);

                return RedirectToAction("Index");
            }

            var resourceTypes = await _resourceTypeService.GetAllMainEntitiesAsync();
            var tags = await _resourceTagService.GetAllMainEntitiesAsync();

            model.ResourceTypes = resourceTypes.Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.TypeName
            }).ToList();
            model.Tags = tags.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.TagName
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var resource = await _coachingResourceService.GetCoachingResourceWithDetailById(id);
            if (resource == null)
            {
                return NotFound();
            }

            var resourceTypes = await _resourceTypeService.GetAllMainEntitiesAsync();
            var tags = await _resourceTagService.GetAllMainEntitiesAsync();

            var updateCoachingResourceViewModel = new UpdateCoachingResourceViewModel
            {
                Id = resource.Data.Id,
                Title = resource.Data.Title,
                Description = resource.Data.Description,
                URL = resource.Data.URL,
                ResourceTypeId = resource.Data.ResourceTypeId,
                ResourceTypes = resourceTypes.Select(rt => new SelectListItem
                {
                    Value = rt.Id.ToString(),
                    Text = rt.TypeName
                }).ToList(),
                Tags = tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.TagName
                }).ToList(),
                SelectedTags = resource.Data.Tags
            };

            return View(updateCoachingResourceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCoachingResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resourceDto = new CreateCoachingResourceWithTagsDto
                {
                    CoachID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    StudentID = HttpContext.Session.GetString("ManagedStudentId"),
                    Title = model.Title,
                    Description = model.Description,
                    URL = model.URL,
                    ResourceTypeId = model.ResourceTypeId,
                };

                var tagIds = model.SelectedTags.Select(int.Parse).ToList();
                await _coachingResourceService.UpdateResourceWithTagsAsync(resourceDto, tagIds, model.Id);

                return RedirectToAction("Index");
            }

            var resourceTypes = await _resourceTypeService.GetAllMainEntitiesAsync();
            var tags = await _resourceTagService.GetAllMainEntitiesAsync();

            model.ResourceTypes = resourceTypes.Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.TypeName
            }).ToList();
            model.Tags = tags.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.TagName
            }).ToList();

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _coachingResourceService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
