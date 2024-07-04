
using DocStor.DataServices;
using DocStor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Maui.Graphics.Platform;

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
                document.ThumbnailBase64 = await GenerateThumbnailBase64Async(filePath);
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

        private async Task<string> GenerateThumbnailBase64Async(string filePath)
        {
            //var extension = Path.GetExtension(filePath);
            //string[] whitelist = ["jpg", "jpeg", "png", "..."];
            using var stream = File.OpenRead(filePath);
            var image = PlatformImage.FromStream(stream);

            var thumbnail = image.Downsize(_dSettings.Value.ThumbnailResolution, _dSettings.Value.ThumbnailResolution, disposeOriginal: true);

            MemoryStream thumbnailStream = new();
            await thumbnail.SaveAsync(thumbnailStream, Microsoft.Maui.Graphics.ImageFormat.Jpeg, quality: 0.9f);

            thumbnailStream.Position = 0;
            return Convert.ToBase64String(thumbnailStream.ToArray());
        }
    }
}
