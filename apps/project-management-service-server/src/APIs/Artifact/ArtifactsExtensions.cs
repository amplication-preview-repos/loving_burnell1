using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Extensions;

public static class ArtifactsExtensions
{
    public static Artifact ToDto(this ArtifactDbModel model)
    {
        return new Artifact
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ArtifactDbModel ToModel(
        this ArtifactUpdateInput updateDto,
        ArtifactWhereUniqueInput uniqueId
    )
    {
        var artifact = new ArtifactDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            artifact.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            artifact.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return artifact;
    }
}
