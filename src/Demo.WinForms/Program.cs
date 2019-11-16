using System;
using Blowdart.UI.WinForms;
using Demo.Examples.Pages;

namespace Demo.WinForms
{
	internal static class Program
    {
        [STAThread]
        private static void Main(params string[] args)
        {
            UiForm.Start(args, "Blowdart.UI Demo", builder =>
            {
	            builder.AddPage("/", FormLayout.Index, IndexPage.Index);
	            builder.AddPage("/counter", FormLayout.Index, CounterPage.Index);
	            builder.AddPage("/fetchdata", FormLayout.Index, FetchDataPage.Index);
	            builder.AddPage("/elements", FormLayout.Index, ElementsPage.Index);
	            builder.AddPage("/editor", FormLayout.Index, EditorPage.Index);
	            builder.AddPage("/styles", FormLayout.Index, StylesPage.Index);
	            builder.AddPage("/i18n", FormLayout.Index, LocalizationPage.Index);
			});
		}
    }
}
