// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor.Rendering
{
	internal sealed class ElementRenderer : 
		IRenderer<BeginElementInstruction, RenderTreeBuilder>,
		IRenderer<EndElementInstruction, RenderTreeBuilder>
	{
		private readonly Stack<string> _pendingElements;
		private readonly ImGui _imGui;

		public ElementRenderer(ImGui imGui)
		{
			_imGui = imGui;
			_pendingElements = new Stack<string>();
		}

		public void Render(RenderTreeBuilder b, BeginElementInstruction instruction)
		{
			_pendingElements.Push(instruction.Name);

			b.OpenElement(instruction.Name);
			
			if (instruction.Id.HasValue)
			{
				var id = instruction.Id.Value;
				b.AddAttribute(HtmlAttributes.Id, id);

				foreach (var @event in _imGui.Ui.GetEventsFor(id))
				{
					switch (@event)
					{
						case HtmlEvents.Mouse.OnClick:
							b.AddAttribute(@event, _imGui.OnClick(id));
							break;
						case HtmlEvents.Mouse.OnDoubleClick:
							b.AddAttribute(@event, _imGui.OnDoubleClick(id));
							break;
					}
				}
			}
		}

		public void Render(RenderTreeBuilder b, EndElementInstruction instruction)
		{
			var elementType = _pendingElements.Peek();
			if (elementType != instruction.Name)
				throw new ArgumentException($"Attempted to end a mismatched element: expected '{instruction.Name}' but was '{elementType}'");
			b.CloseElement();
			_pendingElements.Pop();
		}
	}
}