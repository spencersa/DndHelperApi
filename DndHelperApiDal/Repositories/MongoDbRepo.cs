using DndHelperApiDal.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DndHelperApiDal.Repositories
{
    public interface IMongoDbRepo
    {
        Task CreateCollectionAsync(string collectionName);
        Task<bool> UpsertJsonToCollection(string collectionName, string documentId, string json);
        Task DropCollectionAsync(string collectionName);
        Task<BsonDocument> GetBsonDocumentByDocumentId(string collectionName, string documentId);
        Task<IEnumerable<BsonDocument>> GetAllBsonDocumentsInCollection(string collectionName);
    }

    public class MongoDocument
    {
        public ObjectId _id { get; set; }
    }

    public class MongoDbRepo : IMongoDbRepo
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbRepo(IMongoClient mongoClient, IOptions<ConnectionConfiguration> connectionConfiguration)
        {
            _client = mongoClient;
            _database = _client.GetDatabase(connectionConfiguration.Value.DatabaseName);
        }

        public async Task CreateCollectionAsync(string collectionName)
        {
            await _database.CreateCollectionAsync(collectionName);
        }

        public async Task DropCollectionAsync(string collectionName)
        {
            await _database.DropCollectionAsync(collectionName);
        }

        public async Task<bool> UpsertJsonToCollection(string collectionName, string documentId, string json)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var document = BsonSerializer.Deserialize<BsonDocument>(json);

            var filter = Builders<BsonDocument>.Filter.Eq("Id", documentId);

            if ((await collection.FindAsync(filter)).Any())
            {
                var result = await collection.ReplaceOneAsync(filter, document);
                return result.ModifiedCount > 0;
            }
            else
                await collection.InsertOneAsync(document);

            return true;
        }

        public async Task<BsonDocument> GetBsonDocumentByDocumentId(string collectionName, string documentId)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("Id", documentId);
            return await (await collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BsonDocument>> GetAllBsonDocumentsInCollection(string collectionName)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return await collection.Find(p => true).ToListAsync();
        }
    }
}
