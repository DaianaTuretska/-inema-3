using AutoMapper;
using MediatR;
using MongoDB.Driver;
using CinemaApplication.DTO.Responses;
using CinemaDomain.Entities;
using CinemaInfrastructure.Context;

namespace CinemaApplication.Queries.Services.GetAllServices
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReservationResponse>>
    {
        private readonly IMongoCollection<Reservation> collection;
        
        private readonly IMapper mapper;

        public GetAllReviewsQueryHandler(MongoDbContext dbContext, IMapper mapper)
        {
            collection = dbContext.Collection<Reservation>();
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReservationResponse>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Reservation> results = await collection.Find(_ => true).ToListAsync(cancellationToken: cancellationToken);
            return results.Select(mapper.Map<Reservation, ReservationResponse>);;
        }
    }
}