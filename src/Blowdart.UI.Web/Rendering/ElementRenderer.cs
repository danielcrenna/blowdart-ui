// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ElementRenderer : IWebRenderer<BeginElementInstruction>, IWebRenderer<EndElementInstruction>
    {
        private readonly Stack<ElementType> _pendingElements;

        public ElementRenderer()
        {
            _pendingElements = new Stack<ElementType>();
        }

        public void Render(RenderTreeBuilder b, BeginElementInstruction element)
        {
            Render(b, (RenderInstruction)element);
        }

        public void Render(RenderTreeBuilder b, EndElementInstruction element)
        {
            Render(b, (RenderInstruction) element);
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            switch (instruction)
            {
                case BeginElementInstruction element:
                {
                    _pendingElements.Push(element.Type);

                    var css = element.Style != null ? $" {element.Style}" : string.Empty;
                    switch (element.Type)
                    {
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
						default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
                case EndElementInstruction element:
                {
                    if (_pendingElements.Peek() != element.Type)
                        throw new ArgumentException("Attempted to end a mismatched element");
                    b.CloseElement();
                    _pendingElements.Pop();
                    break;
                }
            }
        }
    }
}
