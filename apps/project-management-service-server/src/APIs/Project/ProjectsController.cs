using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementService.APIs;

[ApiController()]
public class ProjectsController : ProjectsControllerBase
{
    public ProjectsController(IProjectsService service)
        : base(service) { }
}
