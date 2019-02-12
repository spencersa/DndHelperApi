using DndHelperApiDal.Models;
using DndHelperApiDal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DndHelperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("GetDocument")]
        public async Task<string> GetDocument(string collectionName, string documentId)
        {
            return await _documentService.GetDocument(collectionName, documentId);
        }

        [HttpGet]
        [Route("GetAllDocuments")]
        public async Task<string> GetAllDocuments(string collectionName)
        {
            return await _documentService.GetAllDocuments(collectionName);
        }

        [HttpPost]
        [Route("UpsertDocument")]
        public async Task<bool> UpsertDocument([FromBody] DocumentModelDto documentModelDto)
        {
            return await _documentService.UpsertDocument(documentModelDto);
        }
    }
}
