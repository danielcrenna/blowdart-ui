namespace Blowdart.UI.Instructions
{
	public class FilePickerInstruction : RenderInstruction
	{
		public Value128 Id { get; }
		public string Label { get; }

		public FilePickerInstruction(Value128 id, string label)
		{
			Id = id;
			Label = label ?? "Browse";
		}
	}
}