using ProjectManagementService.Infrastructure;

namespace ProjectManagementService.APIs;

public class AuditLogsService : AuditLogsServiceBase
{
    public AuditLogsService(ProjectManagementServiceDbContext context)
        : base(context) { }
}
