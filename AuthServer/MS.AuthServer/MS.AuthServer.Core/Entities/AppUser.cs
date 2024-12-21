using Microsoft.AspNetCore.Identity;
using MS.AuthServer.Core.Enum;

namespace MS.AuthServer.Core.Entities;

public class AppUser : IdentityUser<string>
{
    public string TCKN { get; set; }

    public Gender Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public string UserDescription { get; set; }

    //Profil Resmi ileride eklenebilir. Şimdilik es geçelim.
}

