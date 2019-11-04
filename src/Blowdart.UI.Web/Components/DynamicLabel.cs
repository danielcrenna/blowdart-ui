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
            b.OpenElement(Strings.Label);

            b.AddAttribute(Strings.For, FieldIdentifier.FieldName.ToLowerInvariant());

            if (CssClass != null)
                b.AddAttribute(Strings.Class, CssClass);

            if (AdditionalAttributes != null)
                b.AddMultipleAttributes(AdditionalAttributes);

            if (ChildContent != null)
                b.AddContent(ChildContent);

            b.AddContent(FieldIdentifier.DisplayName());

            b.CloseElement();
        }
	}
}
