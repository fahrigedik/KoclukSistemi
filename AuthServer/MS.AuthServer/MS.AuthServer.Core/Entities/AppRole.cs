using Microsoft.AspNetCore.Identity;

namespace MS.AuthServer.Core.Entities;

public class AppRole : IdentityRole<string>
{
    public string Description { get; set; }

}
