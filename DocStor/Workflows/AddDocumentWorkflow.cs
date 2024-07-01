
using DocStor.DataServices;
using DocStor.Models;

namespace DocStor.Workflows
{
    public class AddDocumentWorkflow(IDocumentsService documentsService)
    {
        private readonly IDocumentsService _documentsService = documentsService;

        public async Task StartFromFileAsync(string filePath)
        {
            DDocument document = new();
            document.Files.Add(new DFile
            {
                Document = document,
                RelativeFilename = filePath
            });

            await _documentsService.AddDocumentAsync(document);
        }

        public async Task StartFromPhotoAsync(string filePath)
        {
            DDocument document = new();
            document.Files.Add(new DFile
            {
                Document = document,
                RelativeFilename = filePath
            });

            await _documentsService.AddDocumentAsync(document);
        }
    }
}
