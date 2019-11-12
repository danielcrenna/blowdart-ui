// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ElementRenderer : 
	    IRenderer<BeginElementInstruction, RenderTreeBuilder>, 
	    IRenderer<EndElementInstruction, RenderTreeBuilder>
    {

		private readonly Stack<ElementType> _pendingElements;

        public ElementRenderer()
        {
            _pendingElements = new Stack<ElementType>();
        }

        public void Render(RenderTreeBuilder b, BeginElementInstruction element)
        {
			_pendingElements.Push(element.Type);

			var css = element.Class != null ? $" {element.Class}" : string.Empty;
			switch (element.Type)
			{
				case ElementType.MainContainer:
					b.BeginDiv($"main-container fullscreen{css}");
					break;
				case ElementType.Container:
					b.BeginDiv($"container{css}");
					break;
				case ElementType.Row:
					b.BeginDiv($"row{css}");
					break;
				case ElementType.Column:
					b.BeginDiv($"column{css}");
					break;
				case ElementType.Main:
					b.BeginDiv($"main{css}");
					break;
				case ElementType.TopRow:
					b.BeginDiv($"top-row px-4{css}");
					break;
				case ElementType.MainContent:
					b.BeginDiv($"content px-4{css}");
					break;
				case ElementType.Section:
					b.BeginSection(css);
					break;
				case ElementType.List:
					b.BeginUnorderedList($"list-group{css}");
					break;
				case ElementType.ListItem:
					b.BeginListItem($"list-group-item {css}");
					break;
				case ElementType.Table:
					b.BeginTable($"{css}");
					break;
				case ElementType.TableRow:
					b.BeginTableRow($"{css}");
					break;
				case ElementType.TableColumn:
					/*
					if (instruction.Ordinal.HasValue)
					{
						b.BeginTableHeader(null);
						b.AddAttribute(Strings.ScreenReaderScope, "row");
						b.AddContent(instruction.Ordinal.ToString());
						b.CloseElement();
					}
					*/
					b.BeginTableColumn($"{css}");
					break;
				case ElementType.Menu:
					b.BeginDiv($"sidebar{css}");
					break;
				case ElementType.TabList:
					b.BeginUnorderedList($"nav nav-tabs{css}");
					b.AddAttribute(HtmlAttributes.Role, "tablist");
					break;
				case ElementType.TabContent:
					b.BeginDiv($"tab-content{css}");
					break;
				case ElementType.Form:
					b.BeginForm($"{css}");
					b.AddAttribute(HtmlAttributes.Method, HttpMethods.Post);
					break;
				case ElementType.Region:
					b.BeginDiv($"{css}");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

        public void Render(RenderTreeBuilder b, EndElementInstruction element)
        {
	        var elementType = _pendingElements.Peek();
	        if (elementType != element.Type)
				throw new ArgumentException($"Attempted to end a mismatched element: expected '{element.Type}' but got '{elementType}'");
			b.CloseElement();
			_pendingElements.Pop();
		}
    }
}
