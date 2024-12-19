using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementService.APIs;

[ApiController()]
public class ArtifactsController : ArtifactsControllerBase
{
    public ArtifactsController(IArtifactsService service)
        : base(service) { }
}
