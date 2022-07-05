using MediatR;
using CinemaApplication.DTO.Responses;

namespace CinemaApplication.Queries.Services.GetByIdService
{
    public class GetByIdReviewQuery : IRequest<ReservationResponse>
    {
        public int Id { get; set; }
    }
}