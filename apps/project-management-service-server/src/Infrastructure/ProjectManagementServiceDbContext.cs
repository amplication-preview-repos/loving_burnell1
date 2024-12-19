using Microsoft.EntityFrameworkCore;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.Infrastructure;

public class ProjectManagementServiceDbContext : DbContext
{
    public ProjectManagementServiceDbContext(
        DbContextOptions<ProjectManagementServiceDbContext> options
    )
        : base(options) { }

    public DbSet<ProjectDbModel> Projects { get; set; }

    public DbSet<FeedbackDbModel> Feedbacks { get; set; }

    public DbSet<ArtifactDbModel> Artifacts { get; set; }

    public DbSet<AuditLogDbModel> AuditLogs { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
