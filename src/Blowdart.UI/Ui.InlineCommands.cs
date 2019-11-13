// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Blowdart.UI.Instructions;
using TypeKitchen;

namespace Blowdart.UI
{
    public partial class Ui
	{
		#region Ids

		private readonly Stack<Value128> _ids = new Stack<Value128>();
		
		public Value128 PushId(Value128 id)
		{
			_ids.Push(id);
			return id;
		}

		public bool TryPopId(out Value128 id)
		{
			if (_ids.Count > 0)
			{
				id = _ids.Pop();
				return true;
			}

			id = default;
			return false;
		}
        
		public Value128 PopId()
		{
			return _ids.Pop();
		}

		#endregion

		#region Options

		private readonly Stack<ElementAlignment> _alignments = new Stack<ElementAlignment>();
		private readonly Stack<InputActivation> _activations = new Stack<InputActivation>();
		private readonly Stack<ElementContext> _contexts = new Stack<ElementContext>();
		private readonly Stack<ElementDecorator> _decorators = new Stack<ElementDecorator>();
		private readonly Stack<FieldType> _fieldTypes = new Stack<FieldType>();
		private readonly Stack<ElementSize> _sizes = new Stack<ElementSize>();

		public ElementContext Push(ElementContext context)
		{
			_contexts.Push(context);
			return context;
		}

		public InputActivation Push(InputActivation activation)
		{
			_activations.Push(activation);
			return activation;
		}

		public ElementAlignment Push(ElementAlignment alignment)
		{
			_alignments.Push(alignment);
			return alignment;
		}

		public ElementSize Push(ElementSize size)
		{
			_sizes.Push(size);
			return size;
		}

		public ElementDecorator Push(ElementDecorator decorator)
		{
			_decorators.Push(decorator);
			return decorator;
		}
		
		public FieldType Push(FieldType type)
		{
			_fieldTypes.Push(type);
			return type;
		}

		internal bool TryPop<T>(out T option)
		{
			if (typeof(T) == typeof(ElementAlignment))
			{
				if (_alignments.Count > 0)
				{
					option = (T) (object) _alignments.Pop();
					return true;
				}
			}

			if (typeof(T) == typeof(ElementContext))
			{
				if (_contexts.Count > 0)
				{
					option = (T) (object) _contexts.Pop();
					return true;
				}
			}

			if (typeof(T) == typeof(ElementSize))
			{
				if (_sizes.Count > 0)
				{
					option = (T) (object) _sizes.Pop();
					return true;
				}
			}

			if (typeof(T) == typeof(InputActivation))
			{
				if (_activations.Count > 0)
				{
					option = (T) (object) _activations.Pop();
					return true;
				}
			}

			if (typeof(T) == typeof(ElementDecorator))
			{
				if (_decorators.Count > 0)
				{
					option = (T) (object) _decorators.Pop();
					return true;
				}
			}

			if (typeof(T) == typeof(FieldType))
			{
				if (_fieldTypes.Count > 0)
				{
					option = (T) (object) _fieldTypes.Pop();
					return true;
				}
			}

			option = default;
			return false;
		}
		
		#endregion

		#region Tables

		private bool _inTable;
		
		public void NextColumn()
		{
			if (!_inTable)
				throw new BlowdartException($"{nameof(NextColumn)} was called outside of a table");
			Instructions.Add(new EndElementInstruction(ElementType.TableColumn));
			Instructions.Add(new BeginElementInstruction(ElementType.TableColumn));
		}

		public void NextColumn(ref int ordinal)
		{
            if(!_inTable)
                throw new BlowdartException($"{nameof(NextColumn)} was called outside of a table");
			Instructions.Add(new EndElementInstruction(ElementType.TableColumn));
			Instructions.Add(new BeginElementInstruction(ElementType.TableColumn, ordinal: ++ordinal));
		}

		#endregion

		#region Menus

		private bool _inMenu;
		private bool _hasMenuItems;

		public void MenuItem(OpenIconicIcons icon, string title, string template)
		{
			if (!_inMenu)
				throw new BlowdartException($"{nameof(MenuItem)} was called outside of a menu");

			if (!_hasMenuItems)
			{
				Instructions.Add(new BeginMenuInstruction());
				_hasMenuItems = true;
			}

			Instructions.Add(new MenuItemInstruction(icon, template, title));
		}

		#endregion

		public void ShowModal(string title)
		{
			var id = HashId($"modal:{title}");
			Instructions.Add(new ShowModalInstruction(id));
		}

		#region Data

		private readonly List<object> _models = new List<object>();

		public T Capture<T>(T model) where T : class
		{
			foreach (var entry in _models.OfType<T>())
				return entry;
			_models.Add(model);
			return model;
		}

		public T Release<T>(T model) where T : class
		{
			_models.Remove(model);
			return model;
		}

		private struct BoolWrapper
		{
			public Type Type;
            public object Model;
            public string Name;
			public bool Value;
		}

		private BoolWrapper _bool;

		public ref bool Bind<T>(Func<T, bool> binder)
		{
			T model = default;
			foreach (var entry in _models.OfType<T>())
				model = entry;
            _bool.Value = binder(model);
			return ref _bool.Value;
		}

		private bool _isBound;

		public ref bool Bind<T>(T model, Expression<Func<T, bool>> binder)
		{
			_isBound = true;
			var accessor = ReadAccessor.Create(typeof(T), out var members);
			var info = GetMemberInfo(model, binder);
			_bool.Type = typeof(T);
			_bool.Name = info.Name;
			_bool.Model = model;
			_bool.Value = (bool) accessor[model, info.Name];
			return ref _bool.Value;
		}

		private void CompletePendingBindings()
		{
			foreach (var model in _models)
			{
				if (_bool.Model != model)
					continue;

				var accessor = WriteAccessor.Create(_bool.Type, out var members);
				_bool.Model = default;
				accessor[model, _bool.Name] = _bool.Value;
				_isBound = false;
			}
		}

		private static MemberInfo GetMemberInfo<T, TProperty>(T source, Expression<Func<T, TProperty>> expression)
		{
			var type = typeof(T);
			if (!(expression.Body is MemberExpression member))
				throw new ArgumentException($"Expression '{expression}' refers to a method, not a property or field.");

			var property = member.Member as PropertyInfo;
			if (property != null)
			{
				if (type != property.ReflectedType && !type.IsSubclassOf(property.ReflectedType ?? throw new InvalidOperationException()))
					throw new ArgumentException($"Expression '{expression}' refers to a property that is not from type {type}.");

				return property;
			}

			var field = member.Member as FieldInfo;
			if (field == null)
				throw new ArgumentException($"Expression '{expression}' did not resolve properly.");

			if (type != field.ReflectedType && !type.IsSubclassOf(field.ReflectedType ?? throw new InvalidOperationException()))
				throw new ArgumentException($"Expression '{expression}' refers to a field that is not from type {type}.");

			return field;
		}

		#endregion
	}
}
