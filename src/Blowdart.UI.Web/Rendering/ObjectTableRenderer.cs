// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ObjectTableRenderer : IWebRenderer<ObjectTableInstruction>
    {
        public void Render(RenderTreeBuilder b, ObjectTableInstruction table)
        {
            b.OpenElement(Strings.Table);
            b.OpenElement(Strings.TableHeaderSection);
            b.OpenElement(Strings.TableRow);
            foreach (var header in table.Headers)
            {
                b.OpenElement(Strings.TableHeader);
                b.AddContent(header);
                b.CloseElement();
            }

            b.CloseElement();
            b.CloseElement();

            b.OpenElement(Strings.TableBodySection);
            foreach (var row in table.Rows)
            {
                b.OpenElement(Strings.TableRow);
                foreach (var column in row.Columns)
                {
                    b.OpenElement(Strings.TableColumn);
                    b.AddContent(column);
                    b.CloseElement();
                }
                b.CloseElement();
            }

            b.CloseElement();

            b.OpenElement(Strings.TableFooterSection);
            b.CloseElement();

            b.CloseElement();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as ObjectTableInstruction);
        }
    }
}
