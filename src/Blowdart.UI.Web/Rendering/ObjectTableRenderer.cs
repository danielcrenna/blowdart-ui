// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ObjectTableRenderer : IRenderer<ObjectTableInstruction, RenderTreeBuilder>
    {
        public void Render(RenderTreeBuilder b, ObjectTableInstruction table)
        {
            b.OpenElement(HtmlElements.Table);
            b.OpenElement(HtmlElements.TableHeaderSection);
            b.OpenElement(HtmlElements.TableRow);
            foreach (var header in table.Headers)
            {
                b.OpenElement(HtmlElements.TableHeader);
                b.AddContent(header);
                b.CloseElement();
            }

            b.CloseElement();
            b.CloseElement();

            b.OpenElement(HtmlElements.TableBodySection);
            foreach (var row in table.Rows)
            {
                b.OpenElement(HtmlElements.TableRow);
                foreach (var column in row.Columns)
                {
                    b.OpenElement(HtmlElements.TableColumn);
                    b.AddContent(column);
                    b.CloseElement();
                }
                b.CloseElement();
            }

            b.CloseElement();

            b.OpenElement(HtmlElements.TableFooterSection);
            b.CloseElement();

            b.CloseElement();
        }
    }
}
