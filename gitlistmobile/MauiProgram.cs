using CommunityToolkit.Maui;
using gitlist.core;
using gitlist.domain;
using Microsoft.Extensions.Logging;

namespace gitlistmobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<IRestApiService, RestApiService>();
		builder.Services.AddTransient<IDataBaseService, DataBaseService>();
		builder.Services.AddTransient<IUserService, UserService>();
		builder.Services.AddTransient<IUserRepository, UserRepository>();
		builder.Services.AddTransient<IAsyncRepository<UserEntity>, AsyncRepository<UserEntity>>();
		builder.Services.AddTransient<IPublicUserRepositoryService, PublicUserRepositoryService>();
		builder.Services.AddTransient<IPublicUserRepositories, PublicUserRepositories>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        var app = builder.Build();
		
		ServiceHelper.Initialize(app.Services);

		return app;
	}
}
