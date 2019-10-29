// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Components
{
	public class DynamicLabel : DynamicElement
	{
		protected override void BuildRenderTree(RenderTreeBuilder b)
		{
            b.OpenElement(b.NextSequence(), Strings.Label);

            b.AddAttribute(b.NextSequence(), Strings.For, FieldIdentifier.FieldName.ToLowerInvariant());

            if (CssClass != null)
                b.AddAttribute(b.NextSequence(), Strings.Class, CssClass);

            if (AdditionalAttributes != null)
                b.AddMultipleAttributes(b.NextSequence(), AdditionalAttributes);

            if (ChildContent != null)
                b.AddContent(b.NextSequence(), ChildContent);

            b.AddContent(b.NextSequence(), FieldIdentifier.DisplayName());

            b.CloseElement();
        }
	}
}
