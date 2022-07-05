using System.Collections.Generic;
using MediatR;
using Application.DTO.Responses;

namespace Application.Queries.Reviews.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReservationResponse>>
    {
        
    }
}