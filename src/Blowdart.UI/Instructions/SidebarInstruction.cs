// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class SidebarInstruction : RenderInstruction
    {
        public string Title { get; }
        public SidebarPage[] Pages { get; }

        public SidebarInstruction(string title, params SidebarPage[] pages)
        {
            Title = title;
            Pages = pages;
        }

        public override string DebuggerDisplay => $"Sidebar: {Title}";
    }
}
