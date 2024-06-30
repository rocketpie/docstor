using DocStor.Database;
using DocStor.Models;

namespace DocStor.DataServices
{
    public interface IDocumentsService
    {
        public Task<IEnumerable<Document>> GetDocumentsAsync();
        public Task AddDocumentAsync(Document newDocument);
    }

    public class DefaultDocumentsService(DataContext dataContext) : IDocumentsService

    {
        private readonly DataContext _dataContext = dataContext;

        public async Task AddDocumentAsync(Document newDocument)
        {
            _dataContext.Documents.Add(newDocument);
            await _dataContext.SaveChangesAsync();
        }

        public Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            IEnumerable<Document> data = _dataContext.Documents.OrderByDescending(d => d.Date);
            return Task.FromResult(data);
        }
    }
}
