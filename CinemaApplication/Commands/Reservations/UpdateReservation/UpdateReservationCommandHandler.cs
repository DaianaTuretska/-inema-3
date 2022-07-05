using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using CinemaApplication.DTO.Requests;
using CinemaApplication.Exceptions;
using CinemaDomain.Entities;
using CinemaInfrastructure;
using CinemaInfrastructure.Context;
using CinemaApplication.Commands.Reservations.UpdateReservation;

namespace CinemaApplication.Commands.Reservations.UpdateReservation
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly CinemaContext sqlDbContext;
        
        private readonly IMongoCollection<Reservation> collection;
        
        private readonly DbSet<Reservation> table;
        
        private readonly IMapper mapper;

        public UpdateReservationCommandHandler(MongoDbContext mongoDbContext, CinemaContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Reservation>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Reservation>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Reservation>.Filter.Eq(c => c.Id, request.ReservationRequest.Id);
            var serviceExists = await collection.Find(filter).AnyAsync(cancellationToken);
            
            if (!serviceExists)
                throw new EntityNotFoundException(nameof(Reservation), request.ReservationRequest.Id);
            
            var entity = mapper.Map<ReservationRequest, Reservation>(request.ReservationRequest);

            await Task.Run(() => table.Update(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}
