// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class AuthPages
	{
		public static void SignIn(Ui ui)
		{
			bool rememberMe = false;

			ui.BeginForm("form-signin");
			{
				ui.BeginRegion("text-center mb-4");
				{
					ui.InlineImage("_content/Blowdart.UI.Web/svg/logo.svg", 72, 72);
					ui.Header(1, "Sign in", "h3 mb-3 font-weight-normal");

					ui.Push(FieldType.Email);
					ui.TextBox(placeholder: "Enter your email address", name: "email");

					ui.Push(FieldType.Password);
					ui.TextBox(placeholder: "Enter your password", name: "password");

					ui.BeginRegion("checkbox mb3");
					ui.CheckBox(ref rememberMe, "Remember me");
					ui.EndRegion();

					ui.Push(ElementSize.Large);
					ui.Push(ElementContext.Primary);
					ui.Button("Sign in");

					ui.TextBlock($"© {DateTimeOffset.UtcNow.Year}");

					ui.EndRegion();
				}

				ui.EndForm();
			}
		}
	}
}