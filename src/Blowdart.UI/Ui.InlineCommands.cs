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
				Instructions.Add(new BeginCollapsibleInstruction());
				_hasMenuItems = true;
			}

			Instructions.Add(new MenuItemInstruction(icon, template, title));
		}

		#endregion
	}
}
