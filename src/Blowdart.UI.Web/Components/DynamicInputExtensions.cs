// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Components
{
    public static class DynamicInputExtensions
    {
        public static void DynamicRender(this DynamicInput input, RenderTreeBuilder b, FieldIdentifier identifier, object value)
        {
            if (!identifier.IsVisible())
                return;

            b.OpenElement(b.NextSequence(), Strings.Input);

            b.AddAttribute(b.NextSequence(), Strings.Id, identifier.FieldName.ToLowerInvariant());

            if (input.CssClass != null)
                b.AddAttribute(b.NextSequence(), Strings.Class, input.CssClass);

            if (identifier.IsReadOnly())
                b.AddAttribute(b.NextSequence(), Strings.Disabled, true);

            if (input.ElementType == typeof(bool))
                b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Checkbox);
            else if (identifier.IsEmailAddress())
                b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Email);
            else if (identifier.IsPassword())
                b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Password);
            else if (identifier.IsDate() || identifier.IsDateTime())
            {
                // HTML5 date picker
                // b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Date);

                // bootstrap-datepicker:
                b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Text);
                b.AddAttribute(b.NextSequence(), Strings.Class, "datepicker");
                b.AddAttribute(b.NextSequence(), "data-provide", "datepicker");
                b.AddAttribute(b.NextSequence(), "data-date-format", identifier.GetDateFormat());
            }

            var prompt = input.Placeholder ?? identifier.Prompt();
            if (!string.IsNullOrWhiteSpace(prompt))
                b.AddAttribute(b.NextSequence(), Strings.Placeholder, prompt);

            var boundValue = BindConverter.FormatValue(value);
            if (boundValue != default)
                b.AddAttribute(b.NextSequence(), Strings.Value, boundValue);

            b.AddAttribute(b.NextSequence(), Events.OnChange, EventCallback.Factory.CreateBinder<string>(input, x => input.CurrentValueAsString = x, input.CurrentValueAsString));
            b.AddMultipleAttributes(b.NextSequence(), input.AdditionalAttributes);

            b.CloseElement();
        }
    }
}
