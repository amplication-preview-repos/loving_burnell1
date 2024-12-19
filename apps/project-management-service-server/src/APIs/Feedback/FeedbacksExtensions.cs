using ProjectManagementService.APIs.Dtos;
using ProjectManagementService.Infrastructure.Models;

namespace ProjectManagementService.APIs.Extensions;

public static class FeedbacksExtensions
{
    public static Feedback ToDto(this FeedbackDbModel model)
    {
        return new Feedback
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FeedbackDbModel ToModel(
        this FeedbackUpdateInput updateDto,
        FeedbackWhereUniqueInput uniqueId
    )
    {
        var feedback = new FeedbackDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            feedback.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            feedback.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return feedback;
    }
}
