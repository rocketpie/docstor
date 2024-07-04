namespace docstor.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            InitializeComponent();

            MainPage = new AppShell();
        }

        /// <summary>
        /// https://github.com/dotnet/maui/discussions/653
        /// </summary>
        private static void CurrentDomain_FirstChanceException(object? sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"********************************** UNHANDLED EXCEPTION! Details: {e.Exception?.ToString()}");
        }
    }
}
