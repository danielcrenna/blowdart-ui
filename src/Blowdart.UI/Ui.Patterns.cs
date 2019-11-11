using System.Collections.Generic;
using Blowdart.UI.Instructions.Patterns;
using Blowdart.UI.Patterns;

namespace Blowdart.UI
{
	partial class Ui
	{
		public void AvatarList(IEnumerable<Avatar> avatars, int size = 32)
		{
			Instructions.Add(new AvatarListInstruction(avatars, size));
		}
	}
}
