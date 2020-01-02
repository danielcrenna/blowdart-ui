using System;
using System.IO;
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
				case InputActivation.OnChange:
				{
					var changed = OnEvent(DomEvents.OnChange, id, out var data);
					if (changed && data != default && data is string dataString)
					{
						int.TryParse(dataString, out value);
						CompletePendingBindings();
					}
					return changed;
				}
				case InputActivation.OnInput:
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

		public bool TextBox(ref string value, string @class = "", string label = "", string labelClass = "", string placeholder = "", string name = "")
		{
			var id = ResolveId();

			TryPop<FieldType>(out var fieldType);
			TryPop<ElementAlignment>(out var alignment);
			TryPop<ElementStyle>(out var style);
			TryPop<InputActivation>(out var activation);
			TryPop<OpenIconicIcons>(out var iconic);
			TryPop<MaterialIcons>(out var material);

			Instructions.Add(new TextBoxInstruction(this, id, fieldType, alignment, style, activation, iconic, material, name, value, _(placeholder), _(label), _inForm, @class, labelClass));
			var changed = OnEvent(activation == InputActivation.OnInput ? DomEvents.OnInput : DomEvents.OnChange, id, out var data);
			if (!changed)
				return false;
			value = (string) data;
			CompletePendingBindings();
			return true;
		}

		public string FilePicker(string label)
		{
			var id = ResolveId();
			Instructions.Add(new FilePickerInstruction(id, _(label)));
			var changed = OnEvent(DomEvents.OnChange, id, out var filePath);
			if (changed)
			{
				CompletePendingBindings();
				var fileName = filePath?.ToString();
				return Path.GetFileName(fileName);
			}
			return null;
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
