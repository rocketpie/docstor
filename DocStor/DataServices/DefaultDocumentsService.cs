using DocStor.Database;
using DocStor.Models;

namespace DocStor.DataServices
{
    public interface IDocumentsService
    {
        public Task<IEnumerable<DDocument>> GetDocumentsAsync();
        public Task AddDocumentAsync(DDocument newDocument);
    }

    public class DefaultDocumentsService(DataContext dataContext) : IDocumentsService

    {
        private readonly DataContext _dataContext = dataContext;

        public async Task AddDocumentAsync(DDocument newDocument)
        {
            _dataContext.Documents.Add(newDocument);
            await _dataContext.SaveChangesAsync();
        }

        public Task<IEnumerable<DDocument>> GetDocumentsAsync()
        {
            IEnumerable<DDocument> data = _dataContext.Documents.OrderByDescending(d => d.Date);
            return Task.FromResult(data);
        }
    }
}
