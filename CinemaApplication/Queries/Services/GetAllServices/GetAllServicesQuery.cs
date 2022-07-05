using System.Collections.Generic;
using MediatR;
using CinemaApplication.DTO.Responses;

namespace CinemaApplication.Queries.Services.GetAllServices
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReservationResponse>>
    {
        
    }
}