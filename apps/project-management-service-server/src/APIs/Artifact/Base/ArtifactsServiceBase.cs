using Microsoft.EntityFrameworkCore;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;
using ProjectManagementService.APIs.Extensions;
using ProjectManagementService.Infrastructure;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs;

public abstract class ArtifactsServiceBase : IArtifactsService
{
    protected readonly ProjectManagementServiceDbContext _context;

    public ArtifactsServiceBase(ProjectManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Artifact
    /// </summary>
    public async Task<Artifact> CreateArtifact(ArtifactCreateInput createDto)
    {
        var artifact = new ArtifactDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            artifact.Id = createDto.Id;
        }

        _context.Artifacts.Add(artifact);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ArtifactDbModel>(artifact.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Artifact
    /// </summary>
    public async Task DeleteArtifact(ArtifactWhereUniqueInput uniqueId)
    {
        var artifact = await _context.Artifacts.FindAsync(uniqueId.Id);
        if (artifact == null)
        {
            throw new NotFoundException();
        }

        _context.Artifacts.Remove(artifact);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Artifacts
    /// </summary>
    public async Task<List<Artifact>> Artifacts(ArtifactFindManyArgs findManyArgs)
    {
        var artifacts = await _context
            .Artifacts.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return artifacts.ConvertAll(artifact => artifact.ToDto());
    }

    /// <summary>
    /// Meta data about Artifact records
    /// </summary>
    public async Task<MetadataDto> ArtifactsMeta(ArtifactFindManyArgs findManyArgs)
    {
        var count = await _context.Artifacts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Artifact
    /// </summary>
    public async Task<Artifact> Artifact(ArtifactWhereUniqueInput uniqueId)
    {
        var artifacts = await this.Artifacts(
            new ArtifactFindManyArgs { Where = new ArtifactWhereInput { Id = uniqueId.Id } }
        );
        var artifact = artifacts.FirstOrDefault();
        if (artifact == null)
        {
            throw new NotFoundException();
        }

        return artifact;
    }

    /// <summary>
    /// Update one Artifact
    /// </summary>
    public async Task UpdateArtifact(
        ArtifactWhereUniqueInput uniqueId,
        ArtifactUpdateInput updateDto
    )
    {
        var artifact = updateDto.ToModel(uniqueId);

        _context.Entry(artifact).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Artifacts.Any(e => e.Id == artifact.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
