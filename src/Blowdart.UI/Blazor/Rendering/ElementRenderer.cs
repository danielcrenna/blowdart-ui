// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Extensions;
using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Blowdart.UI.Blazor.Rendering;

// ReSharper disable once UnusedMember.Global
internal sealed class ElementRenderer(ImGui imGui) :
	IRenderer<BeginElementInstruction, RenderTreeBuilder>,
	IRenderer<EndElementInstruction, RenderTreeBuilder>
{
	private readonly Stack<string> _pendingElements = new();

	public void Render(RenderTreeBuilder b, BeginElementInstruction instruction)
	{
		_pendingElements.Push(instruction.Name);

		b.OpenElement(instruction.Name);

		if (!instruction.Id.HasValue)
			return;

		var id = instruction.Id.Value;
		b.AddAttribute(HtmlAttributes.Id, id.ToShortId());

		foreach (var (eventType, eventData) in imGui.Ui.GetEventsFor(id))
		{
			switch (eventType)
			{
				case HtmlEvents.Mouse.OnClick:
					b.AddAttribute(eventType, imGui.OnClick(id, imGui.Ui.GetEventPredicate<MouseEventArgs>(id, HtmlEvents.Mouse.OnClick)));
					break;
				case HtmlEvents.Mouse.OnDoubleClick:
					b.AddAttribute(eventType, imGui.OnDoubleClick(id));
					break;
				case HtmlEvents.Mouse.OnMouseDown:
					b.AddAttribute(eventType, imGui.OnMouseDown(id, imGui.Ui.GetEventPredicate<MouseEventArgs>(id, HtmlEvents.Mouse.OnMouseDown)));
					break;
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