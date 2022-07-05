using MediatR;
using Application.DTO.Requests;

namespace Application.Commands.Reviews.InsertReview
{
    public class InsertReviewCommand : IRequest
    {
        public ReservationRequest ReviewRequest { get; set; }
    }
}