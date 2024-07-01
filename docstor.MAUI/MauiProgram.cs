using docstor.MAUI.ViewModel;
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
                .AddPages()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.ConfigureSqlite();
            builder.Services.AddSingleton<IDocumentsService, DefaultDocumentsService>();
            builder.Services.AddSingleton<AddDocumentWorkflow>();
            builder.Services.AddSingleton<DocumentsPageViewModel>();

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

        /// <summary>
        /// https://aptabase.com/blog/no-parameterless-constructor-defined-for-type-mainpage-on-net-maui
        /// </summary>
        /// <param name="appBuilder">the MAUI App builder</param>
        private static MauiAppBuilder AddPages(this MauiAppBuilder appBuilder)
        {
            var assembly = typeof(App).Assembly;
            var allTypes = assembly.GetTypes();
            var pageTypes = allTypes.Where(t => typeof(ContentPage).IsAssignableFrom(t));

            // appBuilder.Services.AddSingleton<MainPage>();
            // appBuilder.Services.AddSingleton<DocumentsPage>();
            // ...
            foreach (var pageType in pageTypes)
            {
                appBuilder.Services.AddSingleton(pageType);
            }

            return appBuilder;
        }
    }
}
