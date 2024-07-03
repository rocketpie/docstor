
namespace docstor.MAUI.Controls
{
    /// <summary>
    /// https://stackoverflow.com/questions/71923450/net-maui-showing-image-from-base64-string
    /// </summary>
    public class ImageBase64 : Image
    {
        public static readonly BindableProperty Base64SourceProperty =
            BindableProperty.Create(
                nameof(Base64Source),
                typeof(string),
                typeof(ImageBase64),
                string.Empty,
                propertyChanged: OnBase64SourceChanged);

        public string Base64Source
        {
            set
            {
                SetValue(Base64SourceProperty, value);
            }
            get
            {
                return (string)GetValue(Base64SourceProperty);
            }
        }

        private static void OnBase64SourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String((string)newValue));
            ((Image)bindable).Source = ImageSource.FromStream(() => stream);
        }
    }
}
