using MS.AuthServer.Core.Enum;

namespace MS.AuthServer.Core.DTOs;

public class AppUserDto
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string City { get; set; }
    public DateTime BirthDay { get; set; }
    public string TCKN { get; set; }
    public Gender Gender { get; set; }
}
