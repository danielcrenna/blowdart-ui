// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor
{
	public static partial class RenderTreeBuilderExtensions
	{
		public static RenderTreeBuilder OpenComponent<T>(this RenderTreeBuilder b, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenComponent(b.GetNextSequence(callerMemberName, callerLineNumber), typeof(T));
			return b;
		}

		public static RenderTreeBuilder OpenElement(this RenderTreeBuilder b, string elementName, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenElement(b.GetNextSequence(callerMemberName, callerLineNumber), elementName);
			return b;
		}
		
		public static RenderTreeBuilder AddStyle(this RenderTreeBuilder b, Ui ui, StyleContext context)
		{
			if (!ui.TryPopStyle(out var style) || style == default)
				return b;

			style(context);

			var cssClass = context.ToString().Trim('\'');
			b.AddAttribute(HtmlAttributes.Class, cssClass);

			return b;
		}

		public static RenderTreeBuilder AddAttribute(this RenderTreeBuilder b, string name, bool value,
			[CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
			return b;
		}

		public static RenderTreeBuilder AddAttribute(this RenderTreeBuilder b, string name, string? value,
			[CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
			return b;
		}

		public static RenderTreeBuilder AddAttribute(this RenderTreeBuilder b, string name, object? value,
			[CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
			return b;
		}

		public static RenderTreeBuilder AddContent(this RenderTreeBuilder b, string? textContent,
			[CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), textContent);
			return b;
		}

		private static int GetNextSequence(this RenderTreeBuilder b, string? callerMemberName, int? callerLineNumber)
		{
			var sequence = b.NextSequence(callerLineNumber);
			Trace.TraceInformation($"seq:{callerMemberName}:{callerLineNumber} = {sequence}");
			return sequence;
		}

		private static RenderTreeBuilder ElementInline(this RenderTreeBuilder b, string element, Action<RenderTreeBuilder>? action = default)
		{
			b.OpenElement(element);
			action?.Invoke(b);
			b.CloseElement();
			return b;
		}

		private static readonly ConcurrentStack<string> ElementStack = new ConcurrentStack<string>();
		
		private static RenderTreeBuilder ElementOpen(this RenderTreeBuilder b, string element)
		{
			ElementStack.Push(element);
			b.OpenElement(element);
			return b;
		}

		private static RenderTreeBuilder ElementClose(this RenderTreeBuilder b, string element)
		{
			if (!ElementStack.TryPop(out var next))
				throw new UiException($"Attempted to close '{element}' tag, but could not pop the stack");
			if(element != next)
				throw new UiException($"Attempted to close '{element}' tag, but found '{next}' instead");
			b.CloseElement();
			return b;
		}
	}
}