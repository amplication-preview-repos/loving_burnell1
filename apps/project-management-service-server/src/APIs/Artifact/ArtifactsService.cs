using ProjectManagementService.Infrastructure;

namespace ProjectManagementService.APIs;

public class ArtifactsService : ArtifactsServiceBase
{
    public ArtifactsService(ProjectManagementServiceDbContext context)
        : base(context) { }
}
