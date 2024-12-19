using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
