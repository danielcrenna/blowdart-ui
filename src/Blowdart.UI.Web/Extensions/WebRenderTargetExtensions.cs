// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Extensions
{
    internal static class WebRenderTargetExtensions
    {
        #region div

        public static void BeginDiv(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Div, @class, fragment);
        public static void Div(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Div, @class, fragment);

		#endregion

		#region section

		public static void BeginSection(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Section, @class, fragment);
		public static void Section(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Section, @class, fragment);

		#endregion

		public static bool Button(this RenderTreeBuilder b, ImGui imgui, string @class, Action fragment = null)
        {
            imgui.Ui.NextId();
            var id = imgui.Ui.NextIdHash;
            b.BeginElement(HtmlElements.Button, @class);
            b.AddAttribute(HtmlAttributes.Id, id);
            b.AddAttribute(Events.OnClick, imgui.OnClickCallback(id));
            fragment?.Invoke();
            b.CloseElement();
            return imgui.Ui.OnEvent(Events.OnClick, id, out _);
        }

        public static void BeginSpan(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Span, @class, fragment);
        public static void Span(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Span, @class, fragment);

        public static void BeginUnorderedList(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.UnorderedList, @class, fragment);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.UnorderedList, @class, fragment);

        public static void BeginListItem(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.ListItem, @class);
        public static void ListItem(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.ListItem, @class);
        public static void ListItem(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.ListItem, @class, fragment);
        public static void ListItem(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.ListItem, @class, fragment);

        public static void BeginTable(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.Table, @class);
        public static void Table(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.Table, @class);
        public static void Table(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Table, @class, fragment);
        public static void Table(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Table, @class, fragment);

        public static void BeginTableRow(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.TableRow, @class);
        public static void TableRow(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.TableRow, @class);
        public static void TableRow(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.TableRow, @class, fragment);
        public static void TableRow(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.TableRow, @class, fragment);

        public static void BeginTableColumn(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.TableColumn, @class);
        public static void TableColumn(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.TableColumn, @class);
        public static void TableColumn(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.TableColumn, @class, fragment);
        public static void TableColumn(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.TableColumn, @class, fragment);

        public static void BeginTableHeader(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.TableHeader, @class);
        public static void TableHeader(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.TableHeader, @class);
        public static void TableHeader(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.TableHeader, @class, fragment);
        public static void TableHeader(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.TableHeader, @class, fragment);

		public static void InlineIcon(this RenderTreeBuilder b, OpenIconicIcons icon)
        {
            b.OpenElement(HtmlElements.Span);
			b.Class($"oi oi-{icon.ToCssCase()}");
			b.AriaHidden();
            b.CloseElement();
        }

		public static void InlineImage(this RenderTreeBuilder b, string source, int width, int height)
		{
			b.OpenElement(HtmlElements.Span);
			b.AriaHidden();
			{
				b.OpenElement(HtmlElements.Image);
				b.AddAttribute(HtmlAttributes.Src, source);
				b.AddAttribute(HtmlAttributes.Width, width);
				b.AddAttribute(HtmlAttributes.Height, height);
				b.CloseElement();
			}
			b.CloseElement();
		}

		public static void BeginAnchor(this RenderTreeBuilder b, string @class, string href = "")
		{
			b.BeginElement(HtmlElements.Anchor, @class);
			b.Href(href);
		}
		
		public static void Anchor(this RenderTreeBuilder b, string @class, string href, RenderFragment fragment)
        {
            b.BeginElement(HtmlElements.Anchor, @class);
            b.Href(href);
			fragment?.Invoke(b);
            b.CloseElement();
        }

        public static void Anchor(this RenderTreeBuilder b, string @class, string href, Action fragment)
        {
            b.BeginElement(HtmlElements.Anchor, @class);
            b.Href(href);
			fragment?.Invoke();
            b.CloseElement();
        }

        public static void Anchor(this RenderTreeBuilder b, string @class, string href)
        {
            b.BeginElement(HtmlElements.Anchor, @class);
			b.Href(href);
            b.CloseElement();
        }

        public static void BeginElement(this RenderTreeBuilder b, string elementName, string @class)
        {
            b.OpenElement(elementName);
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
		        b.AddAttribute(HtmlAttributes.Class, @class);
        }

        private static void Href(this RenderTreeBuilder b, string href)
        {
	        if (!string.IsNullOrWhiteSpace(href))
		        b.AddAttribute(HtmlAttributes.Href, href);
        }

		public static void AriaHidden(this RenderTreeBuilder b)
        {
	        b.AddAttribute("aria-hidden", "true");
		}

		#endregion

    }
}
