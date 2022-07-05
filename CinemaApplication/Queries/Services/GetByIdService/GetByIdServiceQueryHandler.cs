using AutoMapper;
using MediatR;
using MongoDB.Driver;
using CinemaApplication.DTO.Responses;
using CinemaApplication.Exceptions;
using CinemaDomain.Entities;
using CinemaInfrastructure.Context;

namespace CinemaApplication.Queries.Services.GetByIdService
{
    public class GetByIdReviewQueryHandler : IRequestHandler<GetByIdReviewQuery, ReservationResponse>
    {
        private readonly IMongoCollection<Reservation> collection;
        
        private readonly IMapper mapper;

        public GetByIdReviewQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Reservation>();
            this.mapper = mapper;
        }

        public async Task<ReservationResponse> Handle(GetByIdReviewQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Reservation>.Filter.Eq(c => c.Id, request.Id);
            var result = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if(result == null)
                throw new EntityNotFoundException(nameof(Reservation), request.Id);
            
            return mapper.Map<Reservation, ReservationResponse>(result);
        }
    }
}
