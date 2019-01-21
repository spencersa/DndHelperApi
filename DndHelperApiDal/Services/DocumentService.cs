using DndHelperApiDal.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Threading.Tasks;

namespace DndHelperApiDal.Services
{
    public interface IDocumentService
    {
        Task<string> GetDocument(string collectionName, string documentId);
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
            var document = await _repository.GetJsonByDocumentId(collectionName, documentId);
            return document
                .GetElement(2)
                .ToJson
                (
                    new JsonWriterSettings { Indent = true }
                );
        }
    }
}
