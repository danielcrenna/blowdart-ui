namespace Blowdart.UI.Gaming.Rendering
{
	internal interface IRenderer<in T> : IRenderer<T, UiGame> 
		where T : RenderInstruction { }
}