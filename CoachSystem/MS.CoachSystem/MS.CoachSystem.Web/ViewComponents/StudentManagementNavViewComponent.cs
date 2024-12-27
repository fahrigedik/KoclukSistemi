using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Service.Services;

namespace MS.CoachSystem.Web.ViewComponents;
public class StudentManagementNavViewComponent : ViewComponent
{
    private readonly StudentService _studentService;

    public StudentManagementNavViewComponent(StudentService studentService)
    {
        _studentService = studentService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var studentId = ViewBag.ManagedStudentId;
        var student = await _studentService.GetStudentsByIdsAsync(new List<string> { studentId });
        return View(student.Data.First());
    }
}

