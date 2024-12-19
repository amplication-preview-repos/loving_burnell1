using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;

namespace ProjectManagementService.APIs;

public interface IAuditLogsService
{
    /// <summary>
    /// Create one AuditLog
    /// </summary>
    public Task<AuditLog> CreateAuditLog(AuditLogCreateInput auditlog);

    /// <summary>
    /// Delete one AuditLog
    /// </summary>
    public Task DeleteAuditLog(AuditLogWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many AuditLogs
    /// </summary>
    public Task<List<AuditLog>> AuditLogs(AuditLogFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about AuditLog records
    /// </summary>
    public Task<MetadataDto> AuditLogsMeta(AuditLogFindManyArgs findManyArgs);

    /// <summary>
    /// Get one AuditLog
    /// </summary>
    public Task<AuditLog> AuditLog(AuditLogWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one AuditLog
    /// </summary>
    public Task UpdateAuditLog(AuditLogWhereUniqueInput uniqueId, AuditLogUpdateInput updateDto);
}
