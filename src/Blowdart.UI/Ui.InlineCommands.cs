// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI.Instructions;

namespace Blowdart.UI
{
    public partial class Ui
	{
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
	}
}