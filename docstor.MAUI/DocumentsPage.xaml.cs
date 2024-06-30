using DocStor.DataServices;
using DocStor.Workflows;

namespace docstor.MAUI;

public partial class DocumentsPage : ContentPage
{
    private readonly IDocumentsService _documentsService;
    private readonly AddDocumentWorkflow _addDocumentWorkflow;

    public DocumentsPage(IDocumentsService documentsService, AddDocumentWorkflow addDocumentWorkflow)
    {
        InitializeComponent();
        _documentsService = documentsService;
        _addDocumentWorkflow = addDocumentWorkflow;

        Loaded += DocumentsPage_LoadedAsync;
    }

    private async void DocumentsPage_LoadedAsync(object? sender, EventArgs e)
    {
        documents = await _documentsService.GetDocumentsAsync();
    }

    IEnumerable<DocStor.Models.Document> documents = [];


    private async void AddDocument_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickMultipleAsync(new PickOptions { PickerTitle = "add a document" });
        if (result == null) return;

        foreach (var file in result)
        {
            await _addDocumentWorkflow.StartFromFileAsync(file.FullPath);
        }
    }


    private async void TakePhoto_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions { Title = "add a document" });
        if (result == null) return;

        await _addDocumentWorkflow.StartFromPhotoAsync(result.FullPath);
    }
}