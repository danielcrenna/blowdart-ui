// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Blazor
{
	internal static class RenderTreeBuilderExtensions
	{
		public static void OpenElement(this RenderTreeBuilder b, string elementName, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenElement(b.GetNextSequence(callerMemberName, callerLineNumber), elementName);
		}

		public static void AddAttribute(this RenderTreeBuilder b, string name, bool value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
		}

		public static void AddAttribute(this RenderTreeBuilder b, string name, string value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
		}

		public static void AddAttribute(this RenderTreeBuilder b, string name, object value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
		}

		public static void AddContent(this RenderTreeBuilder b, string textContent, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), textContent);
		}

		private static int GetNextSequence(this RenderTreeBuilder b, string callerMemberName, int? callerLineNumber)
		{
			var sequence = b.NextSequence(callerLineNumber);
			Trace.TraceInformation($"sequence:{callerMemberName}:{callerLineNumber} = {sequence}");
			return sequence;
		}
	}
}