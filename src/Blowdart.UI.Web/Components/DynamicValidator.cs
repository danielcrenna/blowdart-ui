// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TypeKitchen;

namespace Blowdart.UI.Web.Components
{
    public class DynamicValidator : ComponentBase
	{
		private ValidationMessageStore _messageStore;

        [CascadingParameter] EditContext CurrentEditContext { get; set; }

		protected override void OnInitialized()
		{
			if (CurrentEditContext == null)
				throw new InvalidOperationException($"{nameof(DynamicValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(DynamicValidator)} inside an {nameof(EditForm)}.");

			_messageStore = new ValidationMessageStore(CurrentEditContext);

			CurrentEditContext.OnValidationRequested += (s, e) =>
			{
				_messageStore.Clear();
				ValidateModel((EditContext) s, _messageStore);
			};
			CurrentEditContext.OnFieldChanged += (s, e) =>
			{
				_messageStore.Clear(e.FieldIdentifier);
				ValidateField(CurrentEditContext, _messageStore, e.FieldIdentifier);
			};
		}
        
		private static void ValidateModel(EditContext editContext, ValidationMessageStore messages)
        {
            messages.Clear();

            Validator.ValidateObject(editContext.Model, out var validationResults);
            foreach (var validationResult in validationResults)
            foreach (var memberName in validationResult.MemberNames)
                messages.Add(editContext.Field(memberName), validationResult.ErrorMessage);

            editContext.NotifyValidationStateChanged();
		}

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier)
		{
            var instance = fieldIdentifier.Model;

            var accessor = ReadAccessor.Create(instance, AccessorMemberTypes.Properties, AccessorMemberScope.Public, out var members);
			if (!members.TryGetValue(fieldIdentifier.FieldName, out var member))
				return;

			if (accessor.TryGetValue(instance, member.Name, out var value))
			{
                messages.Clear(fieldIdentifier);
                Validator.ValidateMember(value, member.Name, out var validationResults);
				foreach(var result in validationResults)
					messages.Add(fieldIdentifier, result.ErrorMessage);
			}

			editContext.NotifyValidationStateChanged();
		}
    }
}
