using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using CinemaApplication.Exceptions;
using CinemaDomain.Entities;
using CinemaInfrastructure.Context;

namespace CinemaApplication.Commands.Reservations.DeleteReservation
{
    public class DeleteServiceCommandHandler: IRequestHandler<DeleteReservationCommand>
    {
        private readonly CinemaContext sqlDbContext;
        
        private readonly IMongoCollection<Reservation> collection;
        
        private readonly DbSet<Reservation> table;

        public DeleteServiceCommandHandler(MongoDbContext mongoDbContext, CinemaContext sqlDbContext)
        {
            collection = mongoDbContext.Collection<Reservation>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Reservation>();
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Reservation>.Filter.Eq(c => c.Id, request.Id);
            var entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(Reservation), request.Id);

            await Task.Run(() => table.Remove(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.DeleteOneAsync(filter, cancellationToken);
            
            return Unit.Value;
        }
    }
}