using CommunityToolkit.Maui;
using gitlist.views.Views;
using gitlist.views.ViewModels;
using gitlist.core;
using Microsoft.Extensions.Logging;
using gitlist.data.Services;
using gitlist.data.Repositories;
using gitlist.domain;
using gitlist.core.DataBaseService;

namespace gitlistmobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
#if DEBUG
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            })
#else
			.UseMauiCommunityToolkit(options =>
			{
			   options.SetShouldEnableSnackbarOnWindows(true);
			   options.SetShouldSuppressExceptionsInConverters(true);
			   options.SetShouldSuppressExceptionsInBehaviors(true);
			   options.SetShouldSuppressExceptionsInAnimations(true);
			})
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<IRestApiService, RestApiService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IAsyncRepository<UserEntity>, SqLiteAsyncRepository<UserEntity>>();
        builder.Services.AddTransient<IPublicUserRepositoryService, PublicUserRepositoryService>();
        builder.Services.AddTransient<IPublicUserRepositories, PublicUserRepositories>();


        RegisterViewsAndViewModels(builder.Services);


#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        ServiceContainerInit(app.Services);

        return app;
    }

    static void ServiceContainerInit(IServiceProvider services)
    {
        ServiceContainer.Current.Initialize(services);
        ServiceContainer.Current.BaseApiUrl = "https://api.github.com/";
        ServiceContainer.Current.DbName = "gitlist.db";

        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        ServiceContainer.Current.DbPath = Path.Combine(documentsPath, ServiceContainer.Current.DbName); 
    }

    static void RegisterViewsAndViewModels(in IServiceCollection services)
    {
        services.AddTransient<MainView, MainViewModel>();
        services.AddTransient<DetailsView, DetailsViewModel>();
    }
}