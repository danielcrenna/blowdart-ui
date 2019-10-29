// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class LinkInstruction : RenderInstruction
    {
        public string Href { get; }
        public string Title { get; }

        public LinkInstruction(string href, string title)
        {
            Href = href;
            Title = title;
        }

        public override string DebuggerDisplay => $"a: {Title}";
    }
}
