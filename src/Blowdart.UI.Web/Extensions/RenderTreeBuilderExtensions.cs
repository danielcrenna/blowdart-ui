using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Extensions
{
	[SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
	public static class RenderTreeBuilderExtensions
	{
		public static void AddAttribute(this RenderTreeBuilder b, string name, EventCallback value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
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

		public static void AddAttribute(this RenderTreeBuilder b, string name, bool value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddAttribute(b.GetNextSequence(callerMemberName, callerLineNumber), name, value);
		}

		public static void AddMultipleAttributes(this RenderTreeBuilder b, IEnumerable<KeyValuePair<string, object>> value, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddMultipleAttributes(b.GetNextSequence(callerMemberName, callerLineNumber), value);
		}

		public static void AddContent(this RenderTreeBuilder b, RenderFragment fragment, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), fragment);
		}

		public static void AddContent<T>(this RenderTreeBuilder b, RenderFragment<T> fragment, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), fragment);
		}

		public static void AddContent(this RenderTreeBuilder b, MarkupString markupContent, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), markupContent);
		}

		public static void AddContent(this RenderTreeBuilder b, string textContent, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), textContent);
		}

		public static void AddContent(this RenderTreeBuilder b, object textContent, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.AddContent(b.GetNextSequence(callerMemberName, callerLineNumber), textContent);
		}

		public static void OpenElement(this RenderTreeBuilder b, string elementName, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenElement(b.GetNextSequence(callerMemberName, callerLineNumber), elementName);
		}

		public static void OpenComponent(this RenderTreeBuilder b, Type componentType, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenComponent(b.GetNextSequence(callerMemberName, callerLineNumber), componentType);
		}

		public static void OpenComponent<T>(this RenderTreeBuilder b, [CallerMemberName] string callerMemberName = null, [CallerLineNumber] int? callerLineNumber = null)
		{
			b.OpenComponent(b.GetNextSequence(callerMemberName, callerLineNumber), typeof(T));
		}

		private static int GetNextSequence(this RenderTreeBuilder b, string callerMemberName, int? callerLineNumber)
		{
            var sequence = b.NextSequence(callerMemberName, callerLineNumber);
            Trace.TraceInformation($"sequence:{callerMemberName}:{callerLineNumber} = {sequence}");
			return sequence;
		}
	}
}
