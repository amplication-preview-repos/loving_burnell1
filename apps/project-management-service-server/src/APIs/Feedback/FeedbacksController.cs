using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementService.APIs;

[ApiController()]
public class FeedbacksController : FeedbacksControllerBase
{
    public FeedbacksController(IFeedbacksService service)
        : base(service) { }
}
