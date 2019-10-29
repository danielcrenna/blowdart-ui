// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Rendering
{
    internal sealed class ObjectTableRenderer : IWebRenderer<ObjectTableInstruction>
    {
        public void Render(RenderTreeBuilder b, ObjectTableInstruction table)
        {
            b.OpenElement(b.NextSequence(), Strings.Table);
            b.OpenElement(b.NextSequence(), Strings.TableHeaderSection);
            b.OpenElement(b.NextSequence(), Strings.TableRow);
            foreach (var header in table.Headers)
            {
                b.OpenElement(b.NextSequence(), Strings.TableHeader);
                b.AddContent(b.NextSequence(), header);
                b.CloseElement();
            }

            b.CloseElement();
            b.CloseElement();

            b.OpenElement(b.NextSequence(), Strings.TableBodySection);
            foreach (var row in table.Rows)
            {
                b.OpenElement(b.NextSequence(), Strings.TableRow);
                foreach (var column in row.Columns)
                {
                    b.OpenElement(b.NextSequence(), Strings.TableColumn);
                    b.AddContent(b.NextSequence(), column);
                    b.CloseElement();
                }
                b.CloseElement();
            }

            b.CloseElement();

            b.OpenElement(b.NextSequence(), Strings.TableFooterSection);
            b.CloseElement();

            b.CloseElement();
        }

        public void Render(RenderTreeBuilder b, RenderInstruction instruction)
        {
            Render(b, instruction as ObjectTableInstruction);
        }
    }
}
