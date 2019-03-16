using DndHelperApiDal.Configurations;
using DndHelperApiDal.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
                DndHelperMongoDbConnectionString = "mongodb+srv://DndHelper:pbkqqFLy2WIgnMNu@cluster0-oe5pi.azure.mongodb.net/test?retryWrites=true",
                DatabaseName = "DndHelper"
            });

            var client = new MongoClient(options.Value.DndHelperMongoDbConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);

            _mongoDbRepo = new MongoDbRepo(client, options);
        }

        [Fact]
        public async Task UploadBaseData()
        {
            var directory = new DirectoryInfo(@"C:\temp\Data\");
            var files = directory.GetFiles("*.json");

            foreach (var file in files)
            {
                var json = File.ReadAllText(file.FullName);
                await _mongoDbRepo.UpsertJsonToCollection("Test", Path.GetFileNameWithoutExtension(file.FullName), json);
            }
        }

        public void Dispose()
        {
        }
    }
}
