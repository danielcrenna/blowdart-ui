// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Blowdart.UI.Web")]
[assembly: InternalsVisibleTo("Blowdart.UI.WebAssembly")]
[assembly: InternalsVisibleTo("Blowdart.UI.Gaming")]
[assembly: InternalsVisibleTo("Blowdart.UI.WinForms")]
[assembly: InternalsVisibleTo("Blowdart.UI.Android")]

namespace Blowdart.UI.Internal
{
    internal class InternalsVisibleTo { }
}
