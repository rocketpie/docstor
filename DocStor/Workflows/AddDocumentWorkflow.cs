
using DocStor.DataServices;
using DocStor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace DocStor.Workflows
{
    public class AddDocumentWorkflow(ILogger<AddDocumentWorkflow> logger, IOptions<DSettings> dSettings, IDocumentsService documentsService)
    {
        private readonly ILogger<AddDocumentWorkflow> _logger = logger;
        private readonly IOptions<DSettings> _dSettings = dSettings;
        private readonly IDocumentsService _documentsService = documentsService;

        public async Task StartFromFileAsync(string filePath)
        {
            DDocument document = new();

            document.Files.Add(new DFile
            {
                Document = document,
                RelativeFilename = filePath
            });
            
            try
            {
                document.ThumbnailBase64 = GenerateThumbnailBase64(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("ccd54d {exception}", ex);
            }

            await _documentsService.AddDocumentAsync(document);
        }

        public async Task StartFromPhotoAsync(string filePath) => await StartFromFileAsync(filePath);

        public bool GetThumbnailAbortCallback()
        {
            return true;
        }

        private string GenerateThumbnailBase64(string filePath)
        {
            //var extension = Path.GetExtension(filePath);
            //string[] whitelist = ["jpg", "jpeg", "png", "..."];

            var bitmap = new Bitmap(filePath);

            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(GetThumbnailAbortCallback);
            var thumbnail = bitmap.GetThumbnailImage(_dSettings.Value.ThumbnailResolution, _dSettings.Value.ThumbnailResolution, callback, nint.Zero);

            MemoryStream thumbnailStream = new();
            thumbnail.Save(thumbnailStream, ImageFormat.Jpeg);

            thumbnailStream.Position = 0;
            return Convert.ToBase64String(thumbnailStream.ToArray());
        }
    }
}
