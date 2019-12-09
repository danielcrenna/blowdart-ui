using Blowdart.UI.Instructions;
using ImGuiNET;
using Vector2 = System.Numerics.Vector2;

namespace Blowdart.UI.Gaming.Rendering
{
	internal sealed class Renderer : 
		IRenderer<TextInstruction>,
		IRenderer<TextBlockInstruction>,
		IRenderer<ButtonInstruction>,
		IRenderer<InlineImageInstruction>,
		IRenderer<BeginCollapsibleInstruction>,
		IRenderer<EndCollapsibleInstruction>,
		#region Unused
		IRenderer<BeginElementInstruction>,
		IRenderer<EndElementInstruction>
		#endregion
	{
		private readonly UiGame game;

		public Renderer(UiGame game)
		{
			this.game = game;
		}

		public void Render(UiGame g, TextInstruction instruction)
		{
			ImGui.TextUnformatted(instruction.Text);
		}

		public void Render(UiGame g, TextBlockInstruction instruction)
		{
			ImGui.TextWrapped(instruction.Text);
		}

		public void Render(UiGame g, ButtonInstruction instruction)
		{
			ImGui.PushID(instruction.Id.ToString());
			ImGui.Button(instruction.Text);
			ImGui.PopID();
		}
		
		public void Render(UiGame g, InlineImageInstruction instruction)
		{
			ImGuiRenderer.BeginAnimation();
			ImGui.Image(game.LoadTexture($"Content\\{instruction.Source}"), new Vector2(instruction.Width, instruction.Height));
			ImGuiRenderer.EndFlip(0.05f * g.ticks, 8000);
			ImGui.SameLine();
		}

		public void Render(UiGame g, BeginCollapsibleInstruction instruction)
		{
			ImGuiWindowFlags flags = 0;
			if (instruction.Size == ElementSize.Auto)
				flags |= ImGuiWindowFlags.AlwaysAutoResize;
			else
			{
				ImGui.SetNextWindowContentSize(new Vector2(300, 100));
			}
			ImGui.Begin(instruction.Title, flags);
		}

		public void Render(UiGame g, EndCollapsibleInstruction instruction)
		{
			ImGui.End();
		}

		#region Unused

		public void Render(UiGame g, BeginElementInstruction instruction) { }
		public void Render(UiGame g, EndElementInstruction instruction) { }

		#endregion
	}
}
