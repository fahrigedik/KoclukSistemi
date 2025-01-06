using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MS.AuthServer.Core.Enum;

namespace MS.AuthServer.Core.Entities;

public class AppUser : IdentityUser<string>
{
    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(@"^[0-9]*$")]
    public string? TCKN { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? UserDescription { get; set; }

    //Profil Resmi ileride eklenebilir. Şimdilik es geçelim.
}

