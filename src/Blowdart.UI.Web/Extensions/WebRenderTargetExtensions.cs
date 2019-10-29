// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Extensions
{
    internal static class WebRenderTargetExtensions
    {
        #region div

        public static void BeginDiv(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class) => Element(b, Strings.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.Div, @class, fragment);
        public static void Div(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.Div, @class, fragment);

		#endregion

		#region section

		public static void BeginSection(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class) => Element(b, Strings.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.Section, @class, fragment);
		public static void Section(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.Section, @class, fragment);

		#endregion

		public static bool Button(this RenderTreeBuilder b, ImGui imgui, string @class, Action fragment = null)
        {
            imgui.Ui.NextId();
            var id = imgui.Ui.NextIdHash;
            b.BeginElement(Strings.Button, @class);
            b.AddAttribute(b.NextSequence(), Strings.Id, id);
            b.AddAttribute(b.NextSequence(), Strings.OnClick, imgui.OnClickCallback(id));
            fragment?.Invoke();
            b.CloseElement();
            return imgui.Ui.OnEvent(Strings.OnClick, id);
        }

        public static void BeginSpan(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class) => Element(b, Strings.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.Span, @class, fragment);
        public static void Span(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.Span, @class, fragment);

        public static void BeginUnorderedList(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class) => Element(b, Strings.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.UnorderedList, @class, fragment);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.UnorderedList, @class, fragment);

        public static void BeginListItem(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.ListItem, @class);
        public static void ListItem(this RenderTreeBuilder b, string @class) => Element(b, Strings.ListItem, @class);
        public static void ListItem(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.ListItem, @class, fragment);
        public static void ListItem(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.ListItem, @class, fragment);

        public static void BeginTable(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.Table, @class);
        public static void Table(this RenderTreeBuilder b, string @class) => Element(b, Strings.Table, @class);
        public static void Table(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.Table, @class, fragment);
        public static void Table(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.Table, @class, fragment);

        public static void BeginTableRow(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.TableRow, @class);
        public static void TableRow(this RenderTreeBuilder b, string @class) => Element(b, Strings.TableRow, @class);
        public static void TableRow(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.TableRow, @class, fragment);
        public static void TableRow(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.TableRow, @class, fragment);

        public static void BeginTableColumn(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.TableColumn, @class);
        public static void TableColumn(this RenderTreeBuilder b, string @class) => Element(b, Strings.TableColumn, @class);
        public static void TableColumn(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.TableColumn, @class, fragment);
        public static void TableColumn(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.TableColumn, @class, fragment);

        public static void BeginTableHeader(this RenderTreeBuilder b, string @class) => b.BeginElement(Strings.TableHeader, @class);
        public static void TableHeader(this RenderTreeBuilder b, string @class) => Element(b, Strings.TableHeader, @class);
        public static void TableHeader(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, Strings.TableHeader, @class, fragment);
        public static void TableHeader(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, Strings.TableHeader, @class, fragment);

		public static void InlineIcon(this RenderTreeBuilder b, OpenIconicIcons icon)
        {
            b.OpenElement(b.NextSequence(), Strings.Span);
			b.Class($"oi oi-{icon.ToCssCase()}");
			b.AriaHidden();
            b.CloseElement();
        }

		public static void BeginAnchor(this RenderTreeBuilder b, string @class, string href = "")
		{
			b.BeginElement(Strings.Anchor, @class);
			b.Href(href);
		}
		
		public static void Anchor(this RenderTreeBuilder b, string @class, string href, RenderFragment fragment)
        {
            b.BeginElement(Strings.Anchor, @class);
            b.Href(href);
			fragment?.Invoke(b);
            b.CloseElement();
        }

        public static void Anchor(this RenderTreeBuilder b, string @class, string href, Action fragment)
        {
            b.BeginElement(Strings.Anchor, @class);
            b.Href(href);
			fragment?.Invoke();
            b.CloseElement();
        }

        public static void Anchor(this RenderTreeBuilder b, string @class, string href)
        {
            b.BeginElement(Strings.Anchor, @class);
			b.Href(href);
            b.CloseElement();
        }

        public static void BeginElement(this RenderTreeBuilder b, string elementName, string @class)
        {
            b.OpenElement(b.NextSequence(), elementName);
			b.Class(@class);
        }

        public static void Element(this RenderTreeBuilder b, string elementName, string @class)
        {
            b.BeginElement(elementName, @class);
            b.CloseElement();
        }

        public static void Element(this RenderTreeBuilder b, string elementName, string @class, RenderFragment fragment)
        {
            b.BeginElement(elementName, @class);
            fragment?.Invoke(b);
            b.CloseElement();
        }

        public static void Element(this RenderTreeBuilder b, string elementName, string @class, Action fragment)
        {
            b.BeginElement(elementName, @class);
            fragment?.Invoke();
            b.CloseElement();
        }

		#region Attributes

		public static void Class(this RenderTreeBuilder b, string @class)
        {
	        if (!string.IsNullOrWhiteSpace(@class))
		        b.AddAttribute(b.NextSequence(), Strings.Class, @class);
        }

        private static void Href(this RenderTreeBuilder b, string href)
        {
	        if (!string.IsNullOrWhiteSpace(href))
		        b.AddAttribute(b.NextSequence(), Strings.Href, href);
        }

		public static void AriaHidden(this RenderTreeBuilder b)
        {
	        b.AddAttribute(b.NextSequence(), "aria-hidden", "true");
		}

		#endregion
	}
}
