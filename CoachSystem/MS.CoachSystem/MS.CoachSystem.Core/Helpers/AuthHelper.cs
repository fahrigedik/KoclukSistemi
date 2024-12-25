using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

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
}

