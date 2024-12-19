using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Extensions;

public static class ProjectsExtensions
{
    public static Project ToDto(this ProjectDbModel model)
    {
        return new Project
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ProjectDbModel ToModel(
        this ProjectUpdateInput updateDto,
        ProjectWhereUniqueInput uniqueId
    )
    {
        var project = new ProjectDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            project.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            project.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return project;
    }
}
