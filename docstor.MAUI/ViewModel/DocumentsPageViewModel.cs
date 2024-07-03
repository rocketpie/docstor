using DocStor.Models;
using System.ComponentModel;

namespace docstor.MAUI.ViewModel
{
    public class DocumentsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        IEnumerable<DDocument> _documents = [];
        public IEnumerable<DDocument> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                OnPropertyChanged(nameof(Documents));
            }
        }

        public int ThumbnailSize { get; set; } = 10;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
