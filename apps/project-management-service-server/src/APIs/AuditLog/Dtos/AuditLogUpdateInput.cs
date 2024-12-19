namespace ProjectManagementService.APIs.Dtos;

public class AuditLogUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
