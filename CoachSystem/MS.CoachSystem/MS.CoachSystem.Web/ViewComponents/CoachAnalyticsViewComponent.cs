using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Web.Protos;
using System.Security.Claims;

namespace MS.CoachSystem.Web.ViewComponents
{
    public class CoachAnalyticsViewComponent : ViewComponent
    {
        private readonly Protos.CoachSystem.CoachSystemClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CoachAnalyticsViewComponent> _logger;

        public CoachAnalyticsViewComponent(
            Protos.CoachSystem.CoachSystemClient client,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CoachAnalyticsViewComponent> logger)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }   

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in context");
                }

                _logger.LogInformation($"Requesting analytics for user {userId}");

                var request = new AnalyticsRequest
                {
                    CoachId = userId,
                    StartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"),
                    EndDate = DateTime.Now.ToString("yyyy-MM-dd")
                };

                var response = await _client.GetCoachAnalyticsAsync(
                    request,
                    deadline: DateTime.UtcNow.AddSeconds(30)
                );

                return View(response);
            }
            catch (RpcException ex)
            {
                _logger.LogError($"gRPC call failed: {ex.Status.Detail}");


                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in context");
                }

                _logger.LogInformation($"Requesting analytics for user {userId}");

                var request = new AnalyticsRequest
                {
                    CoachId = userId,
                    StartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"),
                    EndDate = DateTime.Now.ToString("yyyy-MM-dd")
                };

                var response = await _client.GetCoachAnalyticsAsync(
                    request,
                    deadline: DateTime.UtcNow.AddSeconds(30)
                );

                return View(response);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");

                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in context");
                }

                _logger.LogInformation($"Requesting analytics for user {userId}");

                var request = new AnalyticsRequest
                {
                    CoachId = userId,
                    StartDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"),
                    EndDate = DateTime.Now.ToString("yyyy-MM-dd")
                };

                var response = await _client.GetCoachAnalyticsAsync(
                    request,
                    deadline: DateTime.UtcNow.AddSeconds(30)
                );

                return View(response);
            }
        }
    }
}
