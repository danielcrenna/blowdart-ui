using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TypeKitchen;
using TypeKitchen.Creation;

namespace Blowdart.UI
{
	partial class Ui
	{
		private readonly List<object> _models = new List<object>();
		private readonly List<Binding> _bindings = new List<Binding>();

		public T Capture<T>(T model) where T : class
		{
			foreach (var entry in _models.OfType<T>())
				return entry;
			_models.Add(model);
			return model;
		}

		public T Capture<T>() where T : class
		{
			foreach (var entry in _models.OfType<T>())
				return entry;
			var model = GetService(typeof(T)) as T ?? Instancing.CreateInstance<T>();
			_models.Add(model);
			return model;
		}

		public T Release<T>(T model) where T : class
		{
			_models.Remove(model);
			return model;
		}

        [DebuggerDisplay("{" + nameof(DebuggerDisplayName) + "}")]
		private struct Binding
		{
			public string Name;
			public object Model;
			public Type ValueType;

			private string DebuggerDisplayName()
			{
				return $"Bind<{Model.GetType().Name}>(x => ({ValueType.Name}) x.{Name})";
			}
		}

		private bool[] _boundBool = new bool[0];
		public ref bool Bind<T>(T model, Expression<Func<T, bool>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundBool);
		}

		private string[] _boundString = new string[0];
		public ref string Bind<T>(T model, Expression<Func<T, string>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundString);
		}

		private float[] _boundFloat = new float[0];
		public ref float Bind<T>(T model, Expression<Func<T, float>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundFloat);
		}

		private double[] _boundDouble = new double[0];
		public ref double Bind<T>(T model, Expression<Func<T, double>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundDouble);
		}

		private decimal[] _boundDecimal = new decimal[0];
		public ref decimal Bind<T>(T model, Expression<Func<T, decimal>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundDecimal);
		}

		private short[] _boundInt16 = new short[0];
		public ref short Bind<T>(T model, Expression<Func<T, short>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundInt16);
		}
        
		private int[] _boundInt32 = new int[0];
		public ref int Bind<T>(T model, Expression<Func<T, int>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundInt32);
		}

		private long[] _boundInt64 = new long[0];
		public ref long Bind<T>(T model, Expression<Func<T, long>> binder)
		{
			return ref GetBoundRef(model, GetMemberInfo(binder), ref _boundInt64);
		}

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void CompletePendingBindings()
		{
			var bool_i = 0;
			var string_i = 0;
			var short_i = 0;
			var int_i = 0;
			var long_i = 0;
			var float_i = 0;
			var double_i = 0;
			var decimal_i = 0;
            
			foreach (var binding in _bindings)
			{
				if (binding.ValueType == typeof(bool))
				{
					SetBoundRef(binding, ref _boundBool, ref bool_i);
				}
                else if (binding.ValueType == typeof(string))
				{
					SetBoundRef(binding, ref _boundString, ref string_i);
				}
				else if (binding.ValueType == typeof(short))
				{
					SetBoundRef(binding, ref _boundInt16, ref short_i);
				}
				else if (binding.ValueType == typeof(int))
				{
					SetBoundRef(binding, ref _boundInt32, ref int_i);
				}
				else if (binding.ValueType == typeof(long))
				{
					SetBoundRef(binding, ref _boundInt64, ref long_i);
				}
				else if (binding.ValueType == typeof(string))
				{
					SetBoundRef(binding, ref _boundFloat, ref float_i);
				}
				else if (binding.ValueType == typeof(string))
				{
					SetBoundRef(binding, ref _boundDouble, ref double_i);
				}
				else if (binding.ValueType == typeof(string))
				{
					SetBoundRef(binding, ref _boundDecimal, ref decimal_i);
				}
				else
				{
					throw new NotImplementedException($"No binding found for {binding.ValueType.FullName}");
				}
			}
		}
        
        private static void SetBoundRef<T>(Binding binding, ref T[] array, ref int i)
		{
			var model = binding.Model;
			var accessor = WriteAccessor.Create(model);
			accessor[model, binding.Name] = array[i++];
		}

		private ref T GetBoundRef<T>(object model, MemberInfo info, ref T[] array)
		{
			_bindings.Add(new Binding
			{
				Name = info.Name,
				Model = model,
				ValueType = typeof(T)
			});
			var accessor = ReadAccessor.Create(model);
			Array.Resize(ref array, array.Length + 1);
			var value = accessor[model, info.Name];
			array[array.Length - 1] = (T) value;
			return ref array[array.Length - 1];
		}
        
		private void ClearBindings()
		{
            _bindings.Clear();

			Array.Resize(ref _boundBool, 0);
			Array.Resize(ref _boundString, 0);
			Array.Resize(ref _boundFloat, 0);
			Array.Resize(ref _boundDouble, 0);
			Array.Resize(ref _boundDecimal, 0);
			Array.Resize(ref _boundInt16, 0);
			Array.Resize(ref _boundInt32, 0);
			Array.Resize(ref _boundInt64, 0);
		}

		private static MemberInfo GetMemberInfo<T, TProperty>(Expression<Func<T, TProperty>> expression)
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

		internal bool PendingRefresh;

		public void Refresh()
		{
			PendingRefresh = true;
		}
	}
}
