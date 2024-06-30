using DocStor.DataServices;

namespace docstor.MAUI
{
    public partial class MainPage : ContentPage
    {
        private readonly IDocumentsService _documentsService;
        int count = 0;

        public MainPage(IDocumentsService documentsService)
        {
            InitializeComponent();
            _documentsService = documentsService;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            _documentsService.GetDocumentsAsync();

            //Navigation.PushAsync(new DocumentsPage());
        }
    }

}
