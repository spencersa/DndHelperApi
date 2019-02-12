using DndHelperApiDal.Models;
using DndHelperApiDal.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Linq;
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
                .GetElement(2)
                .ToJson
                (
                    new JsonWriterSettings { Indent = true }
                );

            return $"[{json}]";
        }

        public async Task<string> GetAllDocuments(string collectionName)
        {
            var documents = await _repository.GetAllBsonDocumentsInCollection(collectionName);

            var json = documents.Select(document =>
                document.GetElement(2)
                .ToJson
                (
                    new JsonWriterSettings { Indent = true }
                )
            );

            return $"[{string.Join(",", json)}]";
        }

        public async Task<bool> UpsertDocument(DocumentModelDto documentModelDto)
        {
            return await _repository.UpsertJsonToCollection(documentModelDto.CollectionName, documentModelDto.DocumentId, documentModelDto.Json);
        }
    }
}
