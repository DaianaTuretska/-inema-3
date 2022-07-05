using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using CinemaDomain.Entities;

namespace CinemaInfrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase database;

        public MongoDbContext(string mongoUrl)
        {
            MapClasses();
            database = InitDbInstance(mongoUrl);
        }

        private static IMongoDatabase InitDbInstance(string mongoUrl)
        {
            var url = new MongoUrl(mongoUrl);
            var client = new MongoClient(mongoUrl);
            return client.GetDatabase(url.DatabaseName);
        }

        private static void MapClasses()
        {
            BsonClassMap.RegisterClassMap<Reservation>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity: class
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}