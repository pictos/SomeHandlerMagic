using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace SomeHandlerMagic;

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

		EntryImageHandler.RegisterHandler();
#if IOS
		ScrollViewHandler.Mapper.AppendToMapping("ContentSize", (handler, view) =>
		{
			handler.PlatformView.UpdateContentSize(handler.VirtualView.ContentSize);
			handler.PlatformArrange(handler.PlatformView.Frame.ToRectangle());
		});
#endif
		return builder.Build();
	}
}
