using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blowdart.UI.Web.Core
{
	partial class ImGui
	{
		private void OnEvent(Value128 id, string eventType, object data)
		{
			var instructionCount = Ui.Instructions.Count;

			Ui.AddEvent(eventType, id, data);
			Begin();
			Handler(Ui);

			if (Ui.Instructions.Count != instructionCount)
			{
				RunInterop();
			}
		}

		public void OnClick(MouseEventArgs args, Value128 id)
		{
			OnEvent(id, DomEvents.OnClick, null);
		}

		public void OnChange(ChangeEventArgs args, Value128 id)
		{
			OnEvent(id, DomEvents.OnChange, args.Value);
		}

		public void OnInput(ChangeEventArgs args, Value128 id)
		{
			OnEvent(id, DomEvents.OnInput, args.Value);
		}

		public EventCallback<MouseEventArgs> OnClickCallback(Value128 id)
		{
			return EventCallback.Factory.Create<MouseEventArgs>(this, args =>
			{
				OnClick(args, id);
			});
		}

		public EventCallback<ChangeEventArgs> OnChangeCallback(Value128 id)
		{
			return EventCallback.Factory.Create<ChangeEventArgs>(this, args =>
			{
				OnChange(args, id);
			});
		}

		public EventCallback<ChangeEventArgs> OnInputCallback(Value128 id)
		{
			return EventCallback.Factory.Create<ChangeEventArgs>(this, args =>
			{
				OnInput(args, id);
			});
		}
	}
}
