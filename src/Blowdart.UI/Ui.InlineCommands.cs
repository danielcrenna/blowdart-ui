// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
    public partial class Ui
	{
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
		private string _menuTitle;

		public void MenuItem(OpenIconicIcons icon, string title, string template)
		{
			if (!_inMenu)
				throw new BlowdartException($"{nameof(MenuItem)} was called outside of a menu");

			if (!_hasMenuItems)
			{
				Instructions.Add(new BeginCollapsibleInstruction(_menuTitle));
				_hasMenuItems = true;
			}

			Instructions.Add(new MenuItemInstruction(icon, template, title));
		}

		#endregion
	}
}
