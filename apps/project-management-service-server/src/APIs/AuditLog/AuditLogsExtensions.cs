using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Extensions;

public static class AuditLogsExtensions
{
    public static AuditLog ToDto(this AuditLogDbModel model)
    {
        return new AuditLog
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AuditLogDbModel ToModel(
        this AuditLogUpdateInput updateDto,
        AuditLogWhereUniqueInput uniqueId
    )
    {
        var auditLog = new AuditLogDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            auditLog.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            auditLog.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return auditLog;
    }
}
