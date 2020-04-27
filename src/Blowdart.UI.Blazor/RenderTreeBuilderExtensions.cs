// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor
{
	internal static class RenderTreeBuilderExtensions
	{
		public static void BeginElement(this RenderTreeBuilder b, string elementName)
		{
			b.OpenElement(elementName);
		}

		public static void Element(this RenderTreeBuilder b, string elementName)
		{
			b.BeginElement(elementName);
			b.CloseElement();
		}

		public static void OpenElement(this RenderTreeBuilder b, string elementName,
			[CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenElement(b.GetNextSequence(callerMemberName, callerLineNumber), elementName);
		}

		private static int GetNextSequence(this RenderTreeBuilder b, string callerMemberName, int? callerLineNumber)
		{
			var sequence = b.NextSequence(callerMemberName, callerLineNumber);
			Trace.TraceInformation($"sequence:{callerMemberName}:{callerLineNumber} = {sequence}");
			return sequence;
		}

		public static void AddContent(this RenderTreeBuilder b, string textContent,
			[CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), textContent);
		}
	}
}