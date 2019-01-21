using DndHelperApiDal.Configurations;
using DndHelperApiDal.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DndHelperApiDal.Tests
{
    public class MongoDbRepoTests : IDisposable
    {
        private readonly IMongoDbRepo _mongoDbRepo;

        public MongoDbRepoTests()
        {
            var options = Options.Create(new ConnectionConfiguration
            {
                DndHelperMongoDbConnectionString = "mongodb+srv://DndHelper:XXXXXXXXXXXXXXX@cluster0-oe5pi.azure.mongodb.net/test?retryWrites=true",
                DatabaseName = "DndHelper"
            });

            var client = new MongoClient(options.Value.DndHelperMongoDbConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);

            _mongoDbRepo = new MongoDbRepo(client, options);
        }

        [Fact]
        public async Task MongoTest()
        {
            var testJson = JsonConvert.SerializeObject(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Classes.json")));

            await _mongoDbRepo.UpsertJsonToCollection("Test", "Classes", File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Classes.json")));
        }

        public void Dispose()
        {
        }
    }
}
