using Blowdart.UI.FNA;
using Demo.Examples;

namespace Demo.FNA
{
    class Program
    {
	    static void Main(string[] args)
        {
	        Bootstrap.Initialize_FNA();

			using var game = new SampleGame();

			UiGame.Start(args, game, builder =>
			{
				builder.AddPage("/", HelloWorld.Index);
			});
			
			game.Run();
		}
    }
}
