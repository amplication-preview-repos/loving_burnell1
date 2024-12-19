using Microsoft.AspNetCore.Mvc;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;

namespace ProjectManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProjectsControllerBase : ControllerBase
{
    protected readonly IProjectsService _service;

    public ProjectsControllerBase(IProjectsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Project>> CreateProject(ProjectCreateInput input)
    {
        var project = await _service.CreateProject(input);

        return CreatedAtAction(nameof(Project), new { id = project.Id }, project);
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProject([FromRoute()] ProjectWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProject(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Project>>> Projects(
        [FromQuery()] ProjectFindManyArgs filter
    )
    {
        return Ok(await _service.Projects(filter));
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProjectsMeta(
        [FromQuery()] ProjectFindManyArgs filter
    )
    {
        return Ok(await _service.ProjectsMeta(filter));
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Project>> Project([FromRoute()] ProjectWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Project(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProject(
        [FromRoute()] ProjectWhereUniqueInput uniqueId,
        [FromQuery()] ProjectUpdateInput projectUpdateDto
    )
    {
        try
        {
            await _service.UpdateProject(uniqueId, projectUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
