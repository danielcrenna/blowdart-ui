// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blowdart.UI.Web.Components
{
	public class DynamicValidationMessage : DynamicElement
	{
        private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
        private EditContext _previousEditContext;

        public DynamicValidationMessage() => _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();

        [CascadingParameter] protected EditContext CurrentEditContext { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (CurrentEditContext == null)
                throw new InvalidOperationException(
                    $"{GetType()} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {GetType()} inside an {nameof(EditForm)}.");

            if (CurrentEditContext == _previousEditContext)
                return;

            DetachValidationStateChangedListener();
            CurrentEditContext.OnValidationStateChanged += _validationStateChangedHandler;
            _previousEditContext = CurrentEditContext;
        }

        private void DetachValidationStateChangedListener()
        {
            if (_previousEditContext != null)
            {
                _previousEditContext.OnValidationStateChanged -= _validationStateChangedHandler;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            DetachValidationStateChangedListener();
        }

        protected override void BuildRenderTree(RenderTreeBuilder b)
        {
            var messages = CurrentEditContext.GetValidationMessages(FieldIdentifier);

            foreach (var message in messages)
			{
                b.Div(CssClass ?? "validation-message", () =>
                {
                    b.AddMultipleAttributes(b.NextSequence(), AdditionalAttributes);
                    b.AddContent(b.NextSequence(), message);
                });
			}
        }
	}
}
