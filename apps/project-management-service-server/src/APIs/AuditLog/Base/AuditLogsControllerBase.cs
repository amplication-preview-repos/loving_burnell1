using Microsoft.AspNetCore.Mvc;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;

namespace ProjectManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AuditLogsControllerBase : ControllerBase
{
    protected readonly IAuditLogsService _service;

    public AuditLogsControllerBase(IAuditLogsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one AuditLog
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AuditLog>> CreateAuditLog(AuditLogCreateInput input)
    {
        var auditLog = await _service.CreateAuditLog(input);

        return CreatedAtAction(nameof(AuditLog), new { id = auditLog.Id }, auditLog);
    }

    /// <summary>
    /// Delete one AuditLog
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAuditLog([FromRoute()] AuditLogWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAuditLog(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many AuditLogs
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AuditLog>>> AuditLogs(
        [FromQuery()] AuditLogFindManyArgs filter
    )
    {
        return Ok(await _service.AuditLogs(filter));
    }

    /// <summary>
    /// Meta data about AuditLog records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AuditLogsMeta(
        [FromQuery()] AuditLogFindManyArgs filter
    )
    {
        return Ok(await _service.AuditLogsMeta(filter));
    }

    /// <summary>
    /// Get one AuditLog
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AuditLog>> AuditLog(
        [FromRoute()] AuditLogWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.AuditLog(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one AuditLog
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAuditLog(
        [FromRoute()] AuditLogWhereUniqueInput uniqueId,
        [FromQuery()] AuditLogUpdateInput auditLogUpdateDto
    )
    {
        try
        {
            await _service.UpdateAuditLog(uniqueId, auditLogUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
