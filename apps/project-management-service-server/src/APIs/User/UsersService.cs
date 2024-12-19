using ProjectManagementService.Infrastructure;

namespace ProjectManagementService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(ProjectManagementServiceDbContext context)
        : base(context) { }
}
