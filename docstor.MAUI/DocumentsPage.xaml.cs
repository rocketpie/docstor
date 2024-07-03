using docstor.MAUI.ViewModel;
using DocStor.DataServices;
using DocStor.Models;
using DocStor.Workflows;
using Microsoft.Extensions.Options;

namespace docstor.MAUI;

public partial class DocumentsPage : ContentPage
{
    private readonly IOptions<DSettings> _dSettings;
    private readonly IDocumentsService _documentsService;
    private readonly AddDocumentWorkflow _addDocumentWorkflow;

    public DocumentsPage(IOptions<DSettings> dSettings, IDocumentsService documentsService, AddDocumentWorkflow addDocumentWorkflow)
    {
        _dSettings = dSettings;
        _documentsService = documentsService;
        _addDocumentWorkflow = addDocumentWorkflow;
        
        ViewModel.ThumbnailSize = _dSettings.Value.ThumbnailResolution;
        
        InitializeComponent();
        BindingContext = ViewModel;

        Loaded += DocumentsPage_LoadedAsync;
    }

    public DocumentsPageViewModel ViewModel { get; private set; } = new DocumentsPageViewModel();


    private async void DocumentsPage_LoadedAsync(object? sender, EventArgs e)
    {
        ViewModel.Documents = await _documentsService.GetDocumentsAsync();
        Headertext.Text = $"{ViewModel.Documents.Count()} documents";
    }


    private async void AddDocument_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickMultipleAsync(new PickOptions { PickerTitle = "add a document" });
        if (result == null) return;

        foreach (var file in result)
        {
            await _addDocumentWorkflow.StartFromFileAsync(file.FullPath);
        }
        DocumentsPage_LoadedAsync(this, EventArgs.Empty);
    }


    private async void TakePhoto_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions { Title = "add a document" });
        if (result == null) return;

        await _addDocumentWorkflow.StartFromPhotoAsync(result.FullPath);
        DocumentsPage_LoadedAsync(this, EventArgs.Empty);
    }
}