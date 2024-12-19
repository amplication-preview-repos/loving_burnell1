using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementService.APIs;

[ApiController()]
public class AuditLogsController : AuditLogsControllerBase
{
    public AuditLogsController(IAuditLogsService service)
        : base(service) { }
}
