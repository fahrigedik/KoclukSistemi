using MS.CoachSystem.Core.Enum;

namespace MS.CoachSystem.Core.DTOs;
public class CreateStudentUserResponseDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string TCKN { get; set; }
    public Gender Gender { get; set; }
}

