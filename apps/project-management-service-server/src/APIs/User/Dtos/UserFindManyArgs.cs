using Microsoft.AspNetCore.Mvc;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
