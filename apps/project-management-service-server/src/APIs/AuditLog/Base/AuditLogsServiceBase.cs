using Microsoft.EntityFrameworkCore;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;
using ProjectManagementService.APIs.Extensions;
using ProjectManagementService.Infrastructure;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs;

public abstract class AuditLogsServiceBase : IAuditLogsService
{
    protected readonly ProjectManagementServiceDbContext _context;

    public AuditLogsServiceBase(ProjectManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one AuditLog
    /// </summary>
    public async Task<AuditLog> CreateAuditLog(AuditLogCreateInput createDto)
    {
        var auditLog = new AuditLogDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            auditLog.Id = createDto.Id;
        }

        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AuditLogDbModel>(auditLog.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one AuditLog
    /// </summary>
    public async Task DeleteAuditLog(AuditLogWhereUniqueInput uniqueId)
    {
        var auditLog = await _context.AuditLogs.FindAsync(uniqueId.Id);
        if (auditLog == null)
        {
            throw new NotFoundException();
        }

        _context.AuditLogs.Remove(auditLog);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many AuditLogs
    /// </summary>
    public async Task<List<AuditLog>> AuditLogs(AuditLogFindManyArgs findManyArgs)
    {
        var auditLogs = await _context
            .AuditLogs.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return auditLogs.ConvertAll(auditLog => auditLog.ToDto());
    }

    /// <summary>
    /// Meta data about AuditLog records
    /// </summary>
    public async Task<MetadataDto> AuditLogsMeta(AuditLogFindManyArgs findManyArgs)
    {
        var count = await _context.AuditLogs.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one AuditLog
    /// </summary>
    public async Task<AuditLog> AuditLog(AuditLogWhereUniqueInput uniqueId)
    {
        var auditLogs = await this.AuditLogs(
            new AuditLogFindManyArgs { Where = new AuditLogWhereInput { Id = uniqueId.Id } }
        );
        var auditLog = auditLogs.FirstOrDefault();
        if (auditLog == null)
        {
            throw new NotFoundException();
        }

        return auditLog;
    }

    /// <summary>
    /// Update one AuditLog
    /// </summary>
    public async Task UpdateAuditLog(
        AuditLogWhereUniqueInput uniqueId,
        AuditLogUpdateInput updateDto
    )
    {
        var auditLog = updateDto.ToModel(uniqueId);

        _context.Entry(auditLog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AuditLogs.Any(e => e.Id == auditLog.Id))
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
