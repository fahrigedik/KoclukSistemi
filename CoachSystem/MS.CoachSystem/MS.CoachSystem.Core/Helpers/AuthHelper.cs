using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace MS.CoachSystem.Core.Helpers;

public static class AuthHelper
{
    public static void AddAuthorizationHeader(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        var accessToken = httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
        if (!string.IsNullOrEmpty(accessToken))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
    public static string GetCoachId(IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

}

