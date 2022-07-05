using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CinemaInfrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using CinemaApplication.DTO.Requests;
using CinemaDomain.Entities;
using CinemaInfrastructure;

namespace CinemaApplication.Commands.Reservations.InsertReservation
{
    public class InsertReservationCommandHandler : IRequestHandler<InsertReservationCommand>
    {
        private readonly CinemaContext sqlDbContext;
        
        private readonly IMongoCollection<Reservation> collection;
        
        private readonly DbSet<Reservation> table;
        
        private readonly IMapper mapper;

        public InsertReservationCommandHandler(MongoDbContext mongoDbContext, CinemaContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Reservation>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Reservation>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InsertReservationCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ReservationRequest, Reservation>(request.ReviewRequest);
            entity.Id = 0;

            await table.AddAsync(entity, cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}