using MediatR;
using Application.DTO.Requests;

namespace Application.Commands.Reviews.UpdateReview
{
    public class UpdateReviewCommand : IRequest
    {
        public ReservationRequest ReviewRequest { get; set; }
    }
}