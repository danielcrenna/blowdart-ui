// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
    public partial class Ui
	{
		#region ID

		private readonly Stack<Value128> _ids = new Stack<Value128>();
		
		public Value128 PushId(Value128 id)
		{
			_ids.Push(id);
			return id;
		}

		public Value128 PopId()
		{
			return _ids.Pop();
		}

		#endregion

		#region Instructions

		private readonly Stack<RenderInstruction> _instructions = new Stack<RenderInstruction>();

		public T PushInstruction<T>(T instruction) where T : RenderInstruction
		{
			_instructions.Push(instruction);
			return instruction;
		}

		public RenderInstruction PopInstruction()
		{
			return _instructions.Pop();
		}

		#endregion

		#region Options

		private readonly Stack<ElementAlignment> _alignments = new Stack<ElementAlignment>();
		private readonly Stack<InputActivation> _activations = new Stack<InputActivation>();
		private readonly Stack<ElementContext> _contexts = new Stack<ElementContext>();

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

		internal bool TryPop<T>(out T option)
		{
			if (typeof(T) == typeof(InputActivation))
			{
				if (_activations.Count > 0)
				{
					option = (T) (object) _activations.Pop();
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

			if (typeof(T) == typeof(ElementAlignment))
			{
				if (_alignments.Count > 0)
				{
					option = (T) (object) _alignments.Pop();
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
	}
}
