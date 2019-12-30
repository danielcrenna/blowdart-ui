using Blowdart.UI.Gaming;
using Demo.Examples.Pages;

namespace Demo.Game
{
    class Program
    {
	    static void Main(string[] args)
        {
	        Bootstrap.Initialize_FNA();

			UiLoop.Start(args, ()=> new SampleGame(), builder =>
			{
				builder.AddPage("/", IndexPage.Index);
			});
		}
    }
}
