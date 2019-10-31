// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Blowdart.UI.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using TypeKitchen;

namespace Blowdart.UI.Web.Components
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
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

		private EventCallback<ChangeEventArgs> OnChange { get; set; }

        protected override Task OnParametersSetAsync()
        {
	        SetModelAndElementType();
			var onChangeMethod = OnChangeCallbackMethod.MakeGenericMethod(ElementType);
	        OnChange = (EventCallback<ChangeEventArgs>) onChangeMethod.Invoke(this, null);
	        return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder b)
        {
	        if (!FieldIdentifier.IsVisible())
				return;

			b.OpenElement(b.NextSequence(), Strings.Input);

			b.AddAttribute(b.NextSequence(), Strings.Id, FieldIdentifier.FieldName.ToLowerInvariant());

			if (CssClass != null)
				b.AddAttribute(b.NextSequence(), Strings.Class, CssClass);

			if (FieldIdentifier.IsReadOnly())
				b.AddAttribute(b.NextSequence(), Strings.Disabled, true);

			var prompt = Placeholder ?? FieldIdentifier.Prompt();
			if (!string.IsNullOrWhiteSpace(prompt))
				b.AddAttribute(b.NextSequence(), Strings.Placeholder, prompt);

			if (ElementType == typeof(bool))
			{
				b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Checkbox);

				if(Value is bool flag)
					b.AddAttribute(b.NextSequence(), Strings.Checked, flag);

			}
			else if (FieldIdentifier.IsEmailAddress())
			{
				b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Email);
			}
			else if (FieldIdentifier.IsPassword())
			{
				b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Password);
			}
			else if (FieldIdentifier.IsDate() || FieldIdentifier.IsDateTime())
			{
				// HTML5 date picker
				// b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Date);

				// bootstrap-datepicker:
				b.AddAttribute(b.NextSequence(), Strings.Type, Strings.Text);
				b.AddAttribute(b.NextSequence(), Strings.Class, "datepicker");
				b.AddAttribute(b.NextSequence(), "data-provide", "datepicker");
				b.AddAttribute(b.NextSequence(), "data-date-format", FieldIdentifier.GetDateFormat());
			}
			
			var boundValue = BindConverter.FormatValue(Value);
			if (boundValue != default)
				b.AddAttribute(b.NextSequence(), Strings.Value, boundValue);

			b.AddAttribute(b.NextSequence(), Events.OnChange, OnChange);
			b.AddMultipleAttributes(b.NextSequence(), AdditionalAttributes);

			b.CloseElement();
		}

        private static readonly MethodInfo OnChangeCallbackMethod = typeof(DynamicInput).GetMethod(nameof(OnChangeCallback), BindingFlags.Instance | BindingFlags.NonPublic);

        private EventCallback<ChangeEventArgs> OnChangeCallback<T>()
        {
			return EventCallback.Factory.CreateBinder(this, SetCurrentValue, GetCurrentValue<T>());
        }

        private void SetCurrentValue<T>(T value)
        {
	        SetCurrentValueFromString(Transformer.ToString(value, Model, FieldName));
        }

		private T GetCurrentValue<T>()
        {
	        T currentValue = default;
	        var currentValueString = Transformer.ToString(CurrentValue, FieldIdentifier.Model, FieldName);
	        if (Transformer.FromString(ElementType, currentValueString, out var currentValueObject, out _))
		        currentValue = (T) currentValueObject;
	        return currentValue;
        }

        #region Binding

		protected object CurrentValue
		{
			get => Value;
            set
            {
                if (!SetValue(Value, value))
					return;

				OnFieldChanged();
            }
        }

        private void SetCurrentValueFromString(string value)
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
	        foreach (var member in AccessorMembers.Create(ModelType, AccessorMemberTypes.Properties, AccessorMemberScope.Public))
	        {
		        if (member.Name == FieldName)
			        continue;

		        if (member.Display("Default").IsReadOnly)
		        {
			        EditContext.NotifyFieldChanged(EditContext.Field(member.Name));
		        }
	        }

			EditContext.NotifyFieldChanged(FieldIdentifier);
        }

        private bool SetValue(object originalValue, object newValue)
        {
            var hasChanged = !EqualityComparer<object>.Default.Equals(newValue, originalValue);
            if (!hasChanged)
                return false;

            Value = newValue;
            _ = OnValueChanged.InvokeAsync(newValue);
            var write = WriteAccessor.Create(ModelType, AccessorMemberTypes.Properties, AccessorMemberScope.Public);
            write.TrySetValue(Model, FieldIdentifier.FieldName, newValue);
            return true;
        }

        #endregion
    }
}
