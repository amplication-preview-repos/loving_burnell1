using ProjectManagementService.APIs;

namespace ProjectManagementService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IArtifactsService, ArtifactsService>();
        services.AddScoped<IAuditLogsService, AuditLogsService>();
        services.AddScoped<IFeedbacksService, FeedbacksService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
