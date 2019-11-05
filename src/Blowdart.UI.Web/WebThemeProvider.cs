// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Drawing;
using Blowdart.UI.Internal;

namespace Blowdart.UI.Web
{
	public class WebThemeProvider : IThemeProvider
	{
		public Theme GetTheme(string name)
		{
			if (name.Equals("Darkly", StringComparison.OrdinalIgnoreCase))
			{
				return Darkly();
			}

			return null;
		}

		private static Theme Darkly()
		{
			var theme = new Theme
			{
				White = "#fff".ToColor(),
				Gray100 = "#f8f9fa".ToColor(),
				Gray200 = "#ebebeb".ToColor(),
				Gray300 = "#dee2e6".ToColor(),
				Gray400 = "#ced4da".ToColor(),
				Gray500 = "#adb5bd".ToColor(),
				Gray600 = "#999".ToColor(),
				Gray700 = "#444".ToColor(),
				Gray800 = "#303030".ToColor(),
				Gray900 = "#222".ToColor(),
				Black = "#000".ToColor(),
				Blue = "#375a7f".ToColor(),
				Indigo = "#6610f2".ToColor(),
				Purple = "#6f42c1".ToColor(),
				Pink = "#e83e8c".ToColor(),
				Red = "#E74C3C".ToColor(),
				Orange = "#fd7e14".ToColor(),
				Yellow = "#F39C12".ToColor(),
				Green = "#00bc8c".ToColor(),
				Teal = "#20c997".ToColor(),
				Cyan = "#3498DB".ToColor()
			};

			theme.Primary = theme.Blue;
			theme.Secondary = theme.Gray700;
			theme.Success = theme.Green;
			theme.Info = theme.Cyan;
			theme.Warning = theme.Yellow;
			theme.Danger = theme.Red;
			theme.Light = theme.Gray600;
			theme.Dark = theme.Gray800;

			theme.BodyBackground = theme.Gray900;
			theme.BodyForeground = theme.White;
			
			theme.LinkColor = theme.Success;
			
			theme.TableAccent = theme.Gray800;
			theme.TableBorder = theme.Gray700;
            
			theme.InputBorder = Color.Transparent;
			theme.InputGroupAddOnForeground = theme.Gray500;
			theme.InputGroupAddOnBackground = theme.Gray700;
			
			theme.CustomFileForeground = theme.Gray500;
			theme.CustomFileBorder = theme.Gray700;
			
			theme.DropdownBackground = theme.Gray900;
			theme.DropdownBorder = theme.Gray700;
			theme.DropdownDividerBackground = theme.Gray700;
			theme.DropdownLink = theme.White;
			theme.DropdownLinkHover = theme.White;
			theme.DropdownLinkHoverBackground = theme.Primary;
			
			theme.NavLinkDisabled = theme.Gray500;
			
			theme.NavTabsBorder = theme.Gray700;
			theme.NavTabsLinkHoverBorder = theme.NavTabsBorder;
			theme.NavTabsLinkActive = theme.White;
			theme.NavTabsLinkActiveBorder = theme.NavTabsBorder;
			
			theme.NavBarDark = Color.FromArgb((int) (255 * 0.6), theme.White);
			theme.NavBarDarkHover = theme.White;
			theme.NavBarLight = theme.White;
			theme.NavBarLightHover = theme.Success;
			theme.NavBarLightActive = theme.White;
            theme.NavBarLightDisabled = Color.FromArgb((int) (255 * 0.3), theme.White);
            theme.NavBarLightTogglerBorder = Color.FromArgb((int) (255 * 0.1), theme.White);
            
            theme.PaginationForeground = theme.White;
            theme.PaginationBackground = theme.Success;
            theme.PaginationBorder = Color.Transparent;
            theme.PaginationHoverForeground = theme.White;
            theme.PaginationHoverBackground = theme.Success.Lighten(0.1f);
            theme.PaginationHoverBorder = Color.Transparent;
            theme.PaginationActiveBackground = theme.PaginationHoverBackground;
            theme.PaginationActiveBorder = Color.Transparent;
			theme.PaginationDisabledForeground = theme.White;
            theme.PaginationDisabledBackground = theme.Success.Darken(0.15f);
            theme.PaginationDisabledBorder = Color.Transparent;

            theme.JumbotronBackground = theme.Gray800;
            
            theme.CardCapBackground = theme.Gray700;
            theme.CardBackground = theme.Gray800;

            theme.PopoverBackground = theme.Gray800;
            theme.PopoverHeaderBackground = theme.Gray700;
            
            theme.ModalContentBackground = theme.Gray800;
            theme.ModalContentBorder = theme.Gray700;
            theme.ModalHeaderBorder = theme.Gray700;

            theme.ProgressBackground = theme.Gray700;

            theme.ListGroupBackground = theme.Gray800;
            theme.ListGroupBorder = theme.Gray700;
            theme.ListGroupHoverBackground = theme.Gray700;

            theme.BreadcrumbBackground = theme.Gray700;
            theme.CloseForeground = theme.White;
            
			return theme;
		}
	}
}