// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.WebAssembly.Extensions
{
	public static class WebRenderTargetExtensions
    {
        #region div

        public static void BeginDiv(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Div, @class);
        public static void Div(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Div, @class, fragment);
        public static void Div(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Div, @class, fragment);

		#endregion

		#region section

		public static void BeginSection(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Section, @class);
		public static void Section(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Section, @class, fragment);
		public static void Section(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Section, @class, fragment);

		#endregion

		#region form

		public static void BeginForm(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Form, @class);
		public static void Form(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Form, @class);
		public static void Form(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Form, @class, fragment);
		public static void Form(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Form, @class, fragment);

		#endregion

		#region input

		public static void BeginInput(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Input, @class);
		public static void Input(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Input, @class);
		public static void Input(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Input, @class, fragment);
		public static void Input(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Input, @class, fragment);

		#endregion

		#region button

		public static void BeginButton(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Button, @class);
		public static void Button(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Button, @class);
		public static void Button(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Button, @class, fragment);
		public static void Button(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Button, @class, fragment);

		#endregion

		#region label

		public static void BeginLabel(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.Label, @class);
		public static void Label(this RenderTreeBuilder b, string @class = "") => Element(b, HtmlElements.Label, @class);
		public static void Label(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Label, @class, fragment);
		public static void Label(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Label, @class, fragment);

		#endregion

		public static bool Button(this RenderTreeBuilder b, ImGui imgui, string @class, Action fragment = null)
		{
			imgui.Ui.ResolveId();
            var id = imgui.Ui.NextIdHash;
            b.BeginElement(HtmlElements.Button, @class);
            b.AddAttribute(HtmlAttributes.Id, id);
            b.AddAttribute(DomEvents.OnClick, imgui.OnClickCallback(id));
            fragment?.Invoke();
            b.CloseElement();
            return imgui.Ui.OnEvent(DomEvents.OnClick, id, out _);
        }

        public static void BeginSpan(this RenderTreeBuilder b, string @class= "") => b.BeginElement(HtmlElements.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.Span, @class);
        public static void Span(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.Span, @class, fragment);
        public static void Span(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.Span, @class, fragment);

        public static void BeginUnorderedList(this RenderTreeBuilder b, string @class) => b.BeginElement(HtmlElements.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class) => Element(b, HtmlElements.UnorderedList, @class);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, RenderFragment fragment) => Element(b, HtmlElements.UnorderedList, @class, fragment);
        public static void UnorderedList(this RenderTreeBuilder b, string @class, Action fragment) => Element(b, HtmlElements.UnorderedList, @class, fragment);

        public static void BeginListItem(this RenderTreeBuilder b, string @class = "") => b.BeginElement(HtmlElements.ListItem, @class);
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

		public static void InlineIcon(this RenderTreeBuilder b, OpenIconicIcons icon, string @class = "")
		{
			b.BeginElement("i", @class);
			b.Class($"oi oi-{icon.ToIconCase()}");
			b.AriaHidden();
            b.CloseElement();
        }

		public static void InlineIcon(this RenderTreeBuilder b, MaterialIcons icon)
		{
			b.BeginElement("i", "material-icons");
			b.AriaHidden();
			RenderTreeBuilderExtensions.AddContent(b, icon.ToIconCase());
			b.CloseElement();
		}

		public static void BeginAnchor(this RenderTreeBuilder b, string @class = "", string href = "")
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
            RenderTreeBuilderExtensions.OpenElement(b, elementName);
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
		        RenderTreeBuilderExtensions.AddAttribute(b, HtmlAttributes.Class, @class);
        }

        private static void Href(this RenderTreeBuilder b, string href)
        {
	        if (!string.IsNullOrWhiteSpace(href))
		        RenderTreeBuilderExtensions.AddAttribute(b, HtmlAttributes.Href, href);
        }
		
		public static void AriaHidden(this RenderTreeBuilder b)
        {
	        RenderTreeBuilderExtensions.AddAttribute(b, HtmlAttributes.Aria.Hidden, true);
		}

		public static void AriaLabel(this RenderTreeBuilder b, string label)
		{
			RenderTreeBuilderExtensions.AddAttribute(b, HtmlAttributes.Aria.Label, label);
		}

		public static void AriaLabelledBy(this RenderTreeBuilder b, string id)
		{
			RenderTreeBuilderExtensions.AddAttribute(b, HtmlAttributes.Aria.LabelledBy, id);
		}
		
		#endregion

	}
}
