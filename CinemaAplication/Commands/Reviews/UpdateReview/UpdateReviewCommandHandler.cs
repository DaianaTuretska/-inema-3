using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Application.DTO.Requests;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Context;

namespace Application.Commands.Reviews.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly CarRentalContext sqlDbContext;
        
        private readonly IMongoCollection<Review> collection;
        
        private readonly DbSet<Review> table;
        
        private readonly IMapper mapper;

        public UpdateReviewCommandHandler(MongoDbContext mongoDbContext, CarRentalContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Review>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Review>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Review>.Filter.Eq(c => c.Id, request.ReviewRequest.Id);
            var serviceExists = await collection.Find(filter).AnyAsync(cancellationToken);
            
            if (!serviceExists)
                throw new EntityNotFoundException(nameof(Review), request.ReviewRequest.Id);
            
            var entity = mapper.Map<ReservationRequest, Review>(request.ReviewRequest);

            await Task.Run(() => table.Update(entity), cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}