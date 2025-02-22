﻿// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

using Blowdart.UI;

public static partial class WebElements
{
	#region Element-Bound Events

	#region Keyboard

	public static bool KeyDown(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Keyboard.OnKeyDown, out _);
	}

	public static bool KeyPress(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Keyboard.OnKeyPress, out _);
	}

	public static bool KeyUp(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Keyboard.OnKeyUp, out _);
	}

	#endregion

	#region Mouse

	public static bool Click(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnClick, out _);
	}

	public static bool DoubleClick(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnDoubleClick, out _);
	}

	public static bool MouseDown(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnMouseDown, out _);
	}

	public static bool MouseMove(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnMouseMove, out _);
	}

	public static bool MouseOut(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnMouseOut, out _);
	}

	public static bool MouseOver(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnMouseOver, out _);
	}

	public static bool MouseUp(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnMouseUp, out _);
	}

	public static bool Wheel(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Mouse.OnWheel, out _);
	}

	#endregion

	#region Clipboard

	public static bool Copy(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Clipboard.OnCopy, out _);
	}

	public static bool Cut(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Clipboard.OnCut, out _);
	}

	public static bool Paste(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Clipboard.OnPaste, out _);
	}

	#endregion

	#region Drag

	public static bool Drag(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDrag, out _);
	}

	public static bool DragEnd(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDragEnd, out _);
	}

	public static bool DragEnter(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDragEnter, out _);
	}

	public static bool DragLeave(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDragLeave, out _);
	}

	public static bool DragOver(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDragOver, out _);
	}

	public static bool DragStart(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDragStart, out _);
	}

	public static bool Drop(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnDrop, out _);
	}

	public static bool Scroll(this ElementRef e)
	{
		return e.OnEvent(HtmlEvents.Drag.OnScroll, out _);
	}

	#endregion

	#endregion
}