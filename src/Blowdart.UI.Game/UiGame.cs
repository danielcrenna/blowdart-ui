using System;
using Microsoft.Extensions.DependencyInjection;

namespace Blowdart.UI.Game
{
	public class UiGame
	{
		public static void Start(string[] args, Microsoft.Xna.Framework.Game game, Action<BlowdartBuilder> configureAction)
		{
			var pages = new PageMap();
			var services = new GameServiceCollection(game.Services);
			var builder = new BlowdartBuilder(pages, services);
			configureAction(builder);
			services.AddSingleton(pages);
		}
	}
}
