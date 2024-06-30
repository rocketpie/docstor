namespace DocStor.Models
{
    public class Document
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// logical date of document. Might be date of creation, date of arrival, etc.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// eg. 'Anschreiben Kasse - Kind Zahnarztrechnung'
        /// eg. 'Rechnung Siemens WP Trockner' 
        /// eg. 'Mietvertrag Garage Kunde'
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// might just be a single file,
        /// might be eg. /scans/bla/2024-05-21_name-of-document_p1.jpg,
        ///              /scans/bla/2024-05-21_name-of-document_p2.jpg
        /// </summary>
        public ICollection<File> Files { get; } = null!;
        // documents might have other sources. emails? whatsapp chats? web pages? links?

        /// <summary>
        /// data:image/jpeg;base64 [$this]
        /// </summary>
        public string ThumbnailBase64 { get; set; } = "";

    }
}
