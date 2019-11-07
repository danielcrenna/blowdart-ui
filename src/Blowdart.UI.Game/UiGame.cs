using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace Blowdart.UI.FNA
{
	public class UiGame
	{
		public static void Start(string[] args, Game game, Action<BlowdartBuilder> configureAction)
		{
			var pages = new PageMap();
			var services = new GameServiceCollection(game.Services);
			var builder = new BlowdartBuilder(pages, services);
			configureAction(builder);
			services.AddSingleton(pages);
		}
	}
}
