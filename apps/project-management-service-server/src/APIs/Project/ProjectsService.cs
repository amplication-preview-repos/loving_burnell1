using ProjectManagementService.Infrastructure;

namespace ProjectManagementService.APIs;

public class ProjectsService : ProjectsServiceBase
{
    public ProjectsService(ProjectManagementServiceDbContext context)
        : base(context) { }
}
