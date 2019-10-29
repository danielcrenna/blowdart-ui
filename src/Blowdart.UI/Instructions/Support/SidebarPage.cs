// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Blowdart.UI.Instructions
{
    public class SidebarPage
    {
        public OpenIconicIcons Icon { get; set; }
        public string Template { get; set; }
        public string Title { get; set; }

        public SidebarPage(OpenIconicIcons icon, string template, string title)
        {
            Icon = icon;
            Template = template;
            Title = title;
        }
    }
}
