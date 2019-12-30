// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Blowdart.UI;

namespace Demo.Examples.Pages
{
	public class PaymentsPage
	{
		public class ChargeModel
		{
			public string CardNumber { get; set; }
			public string Cardholder { get; set; }
			public int ExpirationMonth { get; set; }
			public int ExpirationYear { get; set; }
			public int SecurityCode { get; set; }
		}

		public static void Index(Ui ui)
		{
			var model = ui.Capture(new ChargeModel());

			ui.BeginRow();
			{
				ui.TextBox(ref ui.Bind(model, x => x.CardNumber), label: "Payment amount");
				ui.TextBox(ref ui.Bind(model, x => x.Cardholder), label: "Name on card");



				ui.EndRow();
			}

			
		}
	}
}