using DndHelperApiDal.Models;
using DndHelperApiDal.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface IDocumentService
    {
        Task<string> GetDocument(string collectionName, string documentId);
        Task<string> GetAllDocuments(string collectionName);
        Task<bool> UpsertDocument(DocumentModelDto doucmentModel);
    }

    public class DocumentService : IDocumentService
    {

        private readonly IMongoDbRepo _repository;

        public DocumentService(IMongoDbRepo repository)
        {
            _repository = repository;
        }

        public async Task<string> GetDocument(string collectionName, string documentId)
        {
            var document = await _repository.GetBsonDocumentByDocumentId(collectionName, documentId);
            var json = document
                .ToJson
                (
                    new JsonWriterSettings { Indent = true }
                );

            return $"[{json}]";
        }

        public async Task<string> GetAllDocuments(string collectionName)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(Path.Combine(executableLocation, "allTables.json"));
        }

        public async Task<bool> UpsertDocument(DocumentModelDto documentModelDto)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(documentModelDto.Json);
            return await _repository.UpsertJsonToCollection(documentModelDto.CollectionName, documentModelDto.DocumentId, json);
        }
    }
}
