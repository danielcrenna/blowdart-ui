using System;
using System.Diagnostics;
using System.Linq;
using Blowdart.UI.Instructions;
using Blowdart.UI.Web.Configuration;
using Microsoft.JSInterop;

namespace Blowdart.UI.Web
{
	partial class ImGui
	{
		private void RunInterop()
		{
			LogToTargets();

			foreach (var _ in Ui.Instructions.OfType<ChangePageInstruction>())
			{
				ChangePage(_.Template);
				break;
			}

			foreach (var _ in Ui.Instructions.OfType<CodeInstruction>())
			{
				SyntaxHighlight();
				break;
			}

			foreach (var _ in Ui.Instructions.OfType<ShowModalInstruction>())
			{
				ShowModal(_.Id);
				break;
			}

			foreach (var _ in Ui.Instructions.OfType<ShowCollapsibleInstruction>())
			{
				ShowCollapsible(_.Id);
				break;
			}
		}

		private void SyntaxHighlight()
		{
			Js.InvokeVoidAsync(Interop.SyntaxHighlight);
		}

		public void ChangePage(string template)
		{
			NavigationManager.NavigateTo(template, true);
		}

		private void LogToTargets()
		{
			foreach (var log in Ui.Instructions.OfType<LogInstruction>())
			{
				LogInstruction(log);
				break;
			}
		}

		public void LogInstruction(LogInstruction log)
		{
			foreach (var target in Options.CurrentValue.LogTargets)
			{
				switch (target)
				{
					case LogTarget.Trace:
						Trace.WriteLine(log.Message);
						break;
					case LogTarget.Console:
						Console.WriteLine(log.Message);
						break;
					case LogTarget.Browser:
						Js.InvokeVoidAsync(Interop.Log, log.Message);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public void ShowModal(Value128 id)
		{
			Js.InvokeVoidAsync(Interop.ShowModal, id.ToString());
		}

		public void ShowCollapsible(Value128 id)
		{
			Js.InvokeVoidAsync(Interop.ShowCollapsible, id.ToString());
		}
	}
}
