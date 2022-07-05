using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Application.DTO.Requests;
using Domain.Entities;
using Infrastructure;

namespace Application.Commands.Reviews.InsertReview
{
    public class InsertReviewCommandHandler : IRequestHandler<InsertReviewCommand>
    {
        private readonly CarRentalContext sqlDbContext;
        
        private readonly IMongoCollection<Review> collection;
        
        private readonly DbSet<Review> table;
        
        private readonly IMapper mapper;

        public InsertReviewCommandHandler(MongoDbContext mongoDbContext, CarRentalContext sqlDbContext, IMapper mapper)
        {
            collection = mongoDbContext.Collection<Review>();

            this.sqlDbContext = sqlDbContext;
            table = this.sqlDbContext.Set<Review>();
            
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(InsertReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ReservationRequest, Review>(request.ReviewRequest);
            entity.Id = 0;

            await table.AddAsync(entity, cancellationToken);
            await sqlDbContext.SaveChangesAsync(cancellationToken);

            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            
            return Unit.Value;
        }
    }
}