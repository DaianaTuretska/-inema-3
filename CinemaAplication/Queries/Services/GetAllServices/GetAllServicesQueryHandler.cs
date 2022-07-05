using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Application.DTO.Responses;
using Domain.Entities;
using Infrastructure.Context;

namespace Application.Queries.Reviews.GetAllReviews
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReservationResponse>>
    {
        private readonly IMongoCollection<Review> collection;
        
        private readonly IMapper mapper;

        public GetAllReviewsQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Review>();
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReservationResponse>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Review> results = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
            return results.Select(mapper.Map<Review, ReservationResponse>);;
        }
    }
}