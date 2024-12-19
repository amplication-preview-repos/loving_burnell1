using ProjectManagementService.Infrastructure;

namespace ProjectManagementService.APIs;

public class FeedbacksService : FeedbacksServiceBase
{
    public FeedbacksService(ProjectManagementServiceDbContext context)
        : base(context) { }
}
