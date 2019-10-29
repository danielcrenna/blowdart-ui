// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using TypeKitchen;

namespace Blowdart.UI.Web.Components
{
    public class DynamicInput : DynamicElement
	{
        private bool _previousParsingAttemptFailed;
        private ValidationMessageStore _parsingValidationMessages;
        private Type _nullableUnderlyingType;

        [Parameter] public string Placeholder { get; set; }
		[Parameter] public object Value { get; set; }
		[Parameter] public EventCallback<object> OnValueChanged { get; set; }
        [CascadingParameter] EditContext CascadedEditContext { get; set; }
        protected EditContext EditContext { get; set; }

        [Inject] public IInputTransformer Transformer { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder b)
		{
            this.DynamicRender(b, FieldIdentifier, Value);
        }
		
		#region Binding

		protected object CurrentValue
		{
			get => Value;
            set
            {
                if (SetValue(Value, value))
                {
                    return;
                }

                OnFieldChanged();
            }
        }

        public string CurrentValueAsString
		{
			get => Transformer.ToString(CurrentValue, FieldIdentifier.Model, FieldName);
			set
			{
				_parsingValidationMessages?.Clear();

				bool parsingFailed;

				if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
				{
					parsingFailed = false;
					CurrentValue = default;
				}
				else if (Transformer.FromString(ElementType, value, out var parsedValue, out var validationErrorMessage))
				{
					parsingFailed = false;
					CurrentValue = parsedValue;
				}
				else
				{
					parsingFailed = true;
					if (_parsingValidationMessages == null)
						_parsingValidationMessages = new ValidationMessageStore(EditContext);

					_parsingValidationMessages.Add(FieldIdentifier, validationErrorMessage);
                    OnFieldChanged();
                }

				if (!parsingFailed && !_previousParsingAttemptFailed)
					return;

				EditContext.NotifyValidationStateChanged();
				_previousParsingAttemptFailed = parsingFailed;
			}
		}

		
        public override async Task SetParametersAsync(ParameterView parameters)
		{
			await base.SetParametersAsync(parameters);

			parameters.SetParameterProperties(this);

			if (EditContext == null)
			{
				EditContext = CascadedEditContext ?? throw new InvalidOperationException(
					              $"{GetType()} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {GetType().FullName} inside an {nameof(EditForm)}.");

				_nullableUnderlyingType = Nullable.GetUnderlyingType(ElementType);
			}
			else if (CascadedEditContext != EditContext)
			{
				throw new InvalidOperationException($"{GetType()} does not support changing the {nameof(EditContext)} dynamically.");
			}
		}

        private void OnFieldChanged()
        {
            EditContext.NotifyFieldChanged(FieldIdentifier);
            foreach (var member in AccessorMembers.Create(ModelType, AccessorMemberTypes.Properties, AccessorMemberScope.Public))
            {
                if (member.Name == FieldName)
                    continue;
                var identifier = new FieldIdentifier(Model, member.Name);
                if (member.Display("Default").IsReadOnly)
                {
                    EditContext.NotifyFieldChanged(identifier);
                }
            }
        }

        private bool SetValue(object originalValue, object newValue)
        {
            var hasChanged = !EqualityComparer<object>.Default.Equals(newValue, originalValue);
            if (!hasChanged)
                return true;

            Value = newValue;
            _ = OnValueChanged.InvokeAsync(newValue);
            var write = WriteAccessor.Create(ModelType, AccessorMemberTypes.Properties, AccessorMemberScope.Public);
            write.TrySetValue(Model, FieldIdentifier.FieldName, newValue);
            return false;
        }

        #endregion
    }
}
