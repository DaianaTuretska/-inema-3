using MediatR;

namespace Application.Commands.Reviews.DeleteReview
{
    public class DeleteReviewCommand : IRequest
    {
        public int Id { get; set; }
    }
}