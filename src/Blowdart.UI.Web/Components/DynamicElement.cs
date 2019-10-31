// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TypeKitchen;

namespace Blowdart.UI.Web.Components
{
	public abstract class DynamicElement : ComponentBase, IDisposable
	{
		private object _previousModel;
		public FieldIdentifier FieldIdentifier { get; private set; }
        [Parameter] public object Model { get; set; }
		[Parameter] public string FieldName { get; set; }

		public Type ModelType { get; set; }
		public Type ElementType { get; set; }
		
		public override async Task SetParametersAsync(ParameterView parameters)
		{
			await base.SetParametersAsync(parameters);

			SetModelAndElementType();
		}

		protected void SetModelAndElementType()
		{
			ModelType = Model.GetType();
			var members = AccessorMembers.Create(ModelType, AccessorMemberTypes.Properties | AccessorMemberTypes.Fields,
				AccessorMemberScope.Public);
			if (members.TryGetValue(FieldName, out var member))
				ElementType = member.Type;
		}

		protected override void OnParametersSet()
		{
			if (Model == _previousModel)
				return;
			FieldIdentifier = new FieldIdentifier(Model, FieldName);
			_previousModel = Model;
		}

        public void Dispose()
        {
            var name = GetType().Name;
            Trace.TraceInformation($"Disposing {name}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Parameter] public virtual string CssClass { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) { }
        }
    }
}
