using System;
using Blowdart.UI.WinForms;
using Demo.Examples;

namespace Demo.WinForms
{
	internal static class Program
    {
        [STAThread]
        private static void Main(params string[] args)
        {
            UiForm.Start(args, "Blowdart.UI Demo", builder =>
            {
	            builder.AddPage("/", FormLayout.Index, HelloWorld.Index);
			});
		}
    }
}
