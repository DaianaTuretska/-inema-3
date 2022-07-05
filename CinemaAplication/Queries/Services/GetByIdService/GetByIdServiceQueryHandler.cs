using AutoMapper;
using MediatR;
using MongoDB.Driver;
using Application.DTO.Responses;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Context;

namespace Application.Queries.Reviews.GetByIdReview
{
    public class GetByIdReviewQueryHandler : IRequestHandler<GetByIdReviewQuery, ReservationResponse>
    {
        private readonly IMongoCollection<Review> collection;
        
        private readonly IMapper mapper;

        public GetByIdReviewQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Review>();
            this.mapper = mapper;
        }

        public async Task<ReservationResponse> Handle(GetByIdReviewQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(c => c.Id, request.Id);
            var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if(result == null)
                throw new EntityNotFoundException(nameof(Review), request.Id);
            
            return mapper.Map<Review, ReservationResponse>(result);
        }
    }
}