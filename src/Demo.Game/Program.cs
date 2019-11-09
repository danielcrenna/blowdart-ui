using Blowdart.UI.Game;
using Demo.Examples;
using Demo.Examples.Pages;

namespace Demo.Game
{
    class Program
    {
	    static void Main(string[] args)
        {
	        Bootstrap.Initialize_FNA();

			using var game = new SampleGame();

			UiGame.Start(args, game, builder =>
			{
				builder.AddPage("/", IndexPage.Index);
			});
			
			game.Run();
		}
    }
}
