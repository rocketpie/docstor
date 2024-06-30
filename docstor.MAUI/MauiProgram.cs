using DocStor.Database;
using DocStor.DataServices;
using DocStor.Workflows;
using Microsoft.Extensions.Logging;

namespace docstor.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.ConfigureSqlite();
            builder.Services.AddSingleton<IDocumentsService, DefaultDocumentsService>();
            builder.Services.AddSingleton<AddDocumentWorkflow>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void ConfigureSqlite(this IServiceCollection services)
        {
            var userDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbFilePath = Path.Combine(userDocuments, ".docstor", "docstor.db");

            if (!Path.Exists(dbFilePath))
            {
                Directory.CreateDirectory(Directory.GetParent(dbFilePath)!.FullName);
            }

            services.AddSqlite<DataContext>($"Data Source={dbFilePath}");
        }
    }
}
