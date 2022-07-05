using MediatR;
using Application.DTO.Responses;

namespace Application.Queries.Reviews.GetByIdReview
{
    public class GetByIdReviewQuery : IRequest<ReservationResponse>
    {
        public int Id { get; set; }
    }
}