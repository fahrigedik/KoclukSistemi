using Grpc.Core;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.GrpcServer; // Proto'dan �retilen kodlar i�in


namespace MS.CoachSystem.GrpcServer.Services
{
public class CoachSystemService : CoachSystem.CoachSystemBase
{
    private readonly ICoachStudentService _coachStudentService;
    private readonly ILogger<CoachSystemService> _logger;

    public CoachSystemService(
        ICoachStudentService coachStudentService,
        ILogger<CoachSystemService> logger)
    {
        _coachStudentService = coachStudentService;
        _logger = logger;
    }

    public override async Task<AnalyticsResponse> GetCoachAnalytics(
        AnalyticsRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation($"Getting analytics for coach: {request.CoachId}");

            var students = await _coachStudentService.GetStudentIdsByCoachIdAsync(request.CoachId);

            return new AnalyticsResponse
            {
                TotalStudents = students.Data.Count(),
                TotalTargets = 0, // TODO: Add target service if needed
                TotalMeetings = 0, // TODO: Add meeting service if needed
                CompletedTargets = 0,
                PendingTargets = 0
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetCoachAnalytics: {ex}");
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}
}