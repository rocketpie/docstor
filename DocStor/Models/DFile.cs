namespace DocStor.Models
{
    public class DFile
    {
        public required DDocument Document { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();


        /// <summary>
        /// eg. /scans/bla/2024-05-21_name-of-document_p1.jpg,
        /// </summary>
        public string RelativeFilename { get; set; } = "";
        /// <summary>
        /// eg. 'Bank Kreditanfrage Auto'
        /// (should be common across all files of the same document)
        /// </summary>
        public string DocumentName { get; set; } = "";

        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// https://learn.microsoft.com/en-us/dotnet/api/system.net.mime?view=net-8.0
        /// </summary>
        public string MediaTypeName { get; set; } = "";

        /// <summary>
        /// file creation date, regardless of 
        /// </summary>
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        /// <summary>
        /// data:image/jpeg;base64 [$this]
        /// </summary>
        public string ThumbnailBase64 { get; set; } = "";

        public string GetFullFilename(string documentsRootLocation)
            => Path.Combine(documentsRootLocation, RelativeFilename);
    }
}
