using Microsoft.AspNetCore.Mvc;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ArtifactFindManyArgs : FindManyInput<Artifact, ArtifactWhereInput> { }
