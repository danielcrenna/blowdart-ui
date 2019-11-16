using System;

namespace Blowdart.UI.WinForms
{
	partial class ImGui
	{
		private void OnEvent(Value128 id, string eventType, object data)
		{
			_ui.AddEvent(eventType, id, data);
			RenderPage();
		}

		public void OnClick(Value128 id)
		{
			OnEvent(id, DomEvents.OnClick, null);
		}

		public EventHandler OnClickCallback(Value128 id)
		{
			return (sender, args) => OnClick(id);
		}
	}
}
