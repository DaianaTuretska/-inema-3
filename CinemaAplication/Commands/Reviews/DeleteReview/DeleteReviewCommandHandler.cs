using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Context;

namespace Application.Commands.Reviews.DeleteReview
{
    public class DeleteServiceCommandHandler: IRequestHandler<DeleteReviewCommand>
    {
        private readonly CinemaContext sqlDbContext;
        
        private readonly IMongoCollection<Review> collection;
        
        private readonly DbSet<Review> table;

        public DeleteServiceCommandHandler(MongoDbContext mongoDbContext, CarRentalContext sqlDbContext)
        {
            collection = mongoDbContext.Collection<Review>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Review>();
        }

        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(c => c.Id, request.Id);
            var entity = await collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
            
            if (entity == null)
                throw new EntityNotFoundException(nameof(Review), request.Id);

            await Task.Run(() => table.Remove(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.DeleteOneAsync(filter, cancellationToken);
            
            return Unit.Value;
        }
    }
}