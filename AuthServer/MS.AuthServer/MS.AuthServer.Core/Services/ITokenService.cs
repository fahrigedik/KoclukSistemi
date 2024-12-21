using MS.AuthServer.Core.Configuration;
using MS.AuthServer.Core.DTOs;
using MS.AuthServer.Core.Entities;

namespace MS.AuthServer.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(AppUser appUser);

    ClientTokenDto CreateClientToken(Client client);
}

