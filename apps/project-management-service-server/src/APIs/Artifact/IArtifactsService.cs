using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;

namespace ProjectManagementService.APIs;

public interface IArtifactsService
{
    /// <summary>
    /// Create one Artifact
    /// </summary>
    public Task<Artifact> CreateArtifact(ArtifactCreateInput artifact);

    /// <summary>
    /// Delete one Artifact
    /// </summary>
    public Task DeleteArtifact(ArtifactWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Artifacts
    /// </summary>
    public Task<List<Artifact>> Artifacts(ArtifactFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Artifact records
    /// </summary>
    public Task<MetadataDto> ArtifactsMeta(ArtifactFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Artifact
    /// </summary>
    public Task<Artifact> Artifact(ArtifactWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Artifact
    /// </summary>
    public Task UpdateArtifact(ArtifactWhereUniqueInput uniqueId, ArtifactUpdateInput updateDto);
}
