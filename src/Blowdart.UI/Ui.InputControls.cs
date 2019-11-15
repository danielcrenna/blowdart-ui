using System;
using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
	partial class Ui
	{
		public bool CheckBox(ref bool value, string label = "")
		{
			var id = ResolveId();
			TryPop<ElementAlignment>(out var alignment);

			Instructions.Add(new CheckBoxInstruction(this, id, _(label), alignment, value, false));
			var clicked = OnEvent(DomEvents.OnClick, id, out var _);
			if (clicked)
			{
				value = !value;
				CompletePendingBindings();
			}

			return clicked;
		}
        
		public bool Slider(ref int value, string label)
		{
			var id = ResolveId();

			TryPop<ElementAlignment>(out var alignment);
			TryPop<InputActivation>(out var activation);

			Instructions.Add(new SliderInstruction(this, id, _(label), alignment, activation, value));
			switch (activation)
			{
				case InputActivation.OnDragEnd:
				{
					var changed = OnEvent(DomEvents.OnChange, id, out var data);
					if (changed && data != default && data is string dataString)
					{
						int.TryParse(dataString, out value);
						CompletePendingBindings();
					}
					return changed;
				}
				case InputActivation.Continuous:
				{
					var changed = OnEvent(DomEvents.OnInput, id, out var data);
					if (changed && data != default && data is string dataString)
					{
						int.TryParse(dataString, out value);
						CompletePendingBindings();
					}
					return changed;
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(activation), activation, null);
			}
		}

		public bool RadioButton(ref bool value, string text)
		{
			var id = ResolveId();

			TryPop<ElementAlignment>(out var alignment);

			Instructions.Add(new RadioButtonInstruction(this, id, _(text), alignment, value));
			var clicked = OnEvent(DomEvents.OnClick, id, out var _);
			if (clicked)
			{
				value = !value;
				CompletePendingBindings();
			}
			return clicked;
		}

		public bool TextBox(ref string value, string label = "", string placeholder = "", string name = "")
		{
			var id = ResolveId();

			TryPop<FieldType>(out var fieldType);
			TryPop<ElementAlignment>(out var alignment);

			Instructions.Add(new TextBoxInstruction(this, id, fieldType, alignment, name, value, _(placeholder), _(label), _inForm));
			var changed = OnEvent(DomEvents.OnChange, id, out var data);
			if (changed)
			{
				value = (string) data;
				CompletePendingBindings();
			}
			return changed;
		}

		#region Implicit Read Only
		
		public void CheckBox(bool value, string label = "")
		{
			var id = ResolveId();
			TryPop<ElementAlignment>(out var alignment);
			Instructions.Add(new CheckBoxInstruction(this, id, _(label), alignment, value, true));
		}

		#endregion
	}
}
