using System;
using Blowdart.UI.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.Gaming
{
	public class UiLoop
	{
		public static void Start(string[] args, Func<UiGame> gameBuilder, Action<BlowdartBuilder> configureAction)
		{
			var game = gameBuilder();

			var pages = new PageMap();
			var services = new GameServiceCollection(game.Services);
			var builder = new BlowdartBuilder(pages, services);
			configureAction(builder);

			services.AddSingleton(pages);
			services.AddSingleton<ILocalizationProvider, LocalizationProvider>();
			services.AddSingleton<ILocalizationStore, MemoryLocalizationStore>();

			game.Run();
		}
	}
}
