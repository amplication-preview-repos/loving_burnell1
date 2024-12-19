using Microsoft.AspNetCore.Mvc;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;

namespace ProjectManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ArtifactsControllerBase : ControllerBase
{
    protected readonly IArtifactsService _service;

    public ArtifactsControllerBase(IArtifactsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Artifact
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Artifact>> CreateArtifact(ArtifactCreateInput input)
    {
        var artifact = await _service.CreateArtifact(input);

        return CreatedAtAction(nameof(Artifact), new { id = artifact.Id }, artifact);
    }

    /// <summary>
    /// Delete one Artifact
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteArtifact([FromRoute()] ArtifactWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteArtifact(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Artifacts
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Artifact>>> Artifacts(
        [FromQuery()] ArtifactFindManyArgs filter
    )
    {
        return Ok(await _service.Artifacts(filter));
    }

    /// <summary>
    /// Meta data about Artifact records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ArtifactsMeta(
        [FromQuery()] ArtifactFindManyArgs filter
    )
    {
        return Ok(await _service.ArtifactsMeta(filter));
    }

    /// <summary>
    /// Get one Artifact
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Artifact>> Artifact(
        [FromRoute()] ArtifactWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Artifact(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Artifact
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateArtifact(
        [FromRoute()] ArtifactWhereUniqueInput uniqueId,
        [FromQuery()] ArtifactUpdateInput artifactUpdateDto
    )
    {
        try
        {
            await _service.UpdateArtifact(uniqueId, artifactUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
