using Grpc.Core;
using MS.CoachSystem.Core.Services;
using MS.CoachSystem.GrpcServer; // Proto'dan �retilen kodlar i�in


namespace MS.CoachSystem.GrpcServer.Services
{
    public class CoachSystemService : CoachSystem.CoachSystemBase
    {
        private readonly ICoachStudentService _coachStudentService;
        private readonly ICoachingResourceService _coachingResourceService;
        private readonly ICoachingSessionService _coachingSessionService;
        private readonly IGoalService _goalService;
        private readonly IUserTaskService _userTaskService;

        private readonly ILogger<CoachSystemService> _logger;

        public CoachSystemService(
            ICoachStudentService coachStudentService,
            ILogger<CoachSystemService> logger,
            ICoachingResourceService coachingResourceService,
            ICoachingSessionService coachingSessionService,
            IGoalService goalService,
            IUserTaskService userTaskService)
        {
            _coachStudentService = coachStudentService;
            _logger = logger;
            _coachingResourceService = coachingResourceService;
            _coachingSessionService = coachingSessionService;
            _goalService = goalService;
            _userTaskService = userTaskService;
        }

        public override async Task<AnalyticsResponse> GetCoachAnalytics(
            AnalyticsRequest request,
            ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Getting analytics for coach: {request.CoachId}");

                var studentCount = await _coachStudentService.GetCountByCoachId(request.CoachId);
                var coachingSessionCount = await _coachingSessionService.GetCountByCoachId(request.CoachId);
                var coachingResourceCount = await _coachingResourceService.GetCountByCoachId(request.CoachId);
                var goalCount = await _goalService.GetCountByCoachId(request.CoachId);
                var userTaskCount = await _userTaskService.GetCountByCoachId(request.CoachId);

                return new AnalyticsResponse
                {
                    CoachStudentCount = studentCount,
                    CoachingSessionCount = coachingSessionCount,
                    CoachingResourceCount = coachingResourceCount,
                    GoalCount = goalCount,
                    UserTaskCount = userTaskCount
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