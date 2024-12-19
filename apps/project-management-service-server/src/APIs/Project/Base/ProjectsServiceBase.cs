using Microsoft.EntityFrameworkCore;
using ProjectManagementService.APIs;
using ProjectManagementService.APIs.Common;
using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.APIs.Errors;
using ProjectManagementService.APIs.Extensions;
using ProjectManagementService.Infrastructure;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs;

public abstract class ProjectsServiceBase : IProjectsService
{
    protected readonly ProjectManagementServiceDbContext _context;

    public ProjectsServiceBase(ProjectManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Project
    /// </summary>
    public async Task<Project> CreateProject(ProjectCreateInput createDto)
    {
        var project = new ProjectDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            project.Id = createDto.Id;
        }

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProjectDbModel>(project.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Project
    /// </summary>
    public async Task DeleteProject(ProjectWhereUniqueInput uniqueId)
    {
        var project = await _context.Projects.FindAsync(uniqueId.Id);
        if (project == null)
        {
            throw new NotFoundException();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Projects
    /// </summary>
    public async Task<List<Project>> Projects(ProjectFindManyArgs findManyArgs)
    {
        var projects = await _context
            .Projects.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return projects.ConvertAll(project => project.ToDto());
    }

    /// <summary>
    /// Meta data about Project records
    /// </summary>
    public async Task<MetadataDto> ProjectsMeta(ProjectFindManyArgs findManyArgs)
    {
        var count = await _context.Projects.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Project
    /// </summary>
    public async Task<Project> Project(ProjectWhereUniqueInput uniqueId)
    {
        var projects = await this.Projects(
            new ProjectFindManyArgs { Where = new ProjectWhereInput { Id = uniqueId.Id } }
        );
        var project = projects.FirstOrDefault();
        if (project == null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    /// <summary>
    /// Update one Project
    /// </summary>
    public async Task UpdateProject(ProjectWhereUniqueInput uniqueId, ProjectUpdateInput updateDto)
    {
        var project = updateDto.ToModel(uniqueId);

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Projects.Any(e => e.Id == project.Id))
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
