using System.Text;
using Blowdart.UI.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TypeKitchen;

namespace Blowdart.UI.Web.Controllers
{
	public class WebAppController : Controller
	{
		private readonly IOptionsSnapshot<BlowdartOptions> _options;

		public WebAppController(IOptionsSnapshot<BlowdartOptions> options)
		{
			_options = options;
		}

		[Route("robots.txt")]
		public ContentResult Robots()
		{
			var content = Pooling.StringBuilderPool.Scoped(sb =>
			{
				sb.AppendLine($"# NoIndex = {_options.Value.App.RobotsTxt.DisallowAll.ToString().ToLower()}");
				sb.AppendLine("user-agent: *");
				sb.AppendLine(_options.Value.App.RobotsTxt.DisallowAll ? "Disallow: /" : "Disallow:");
			});

			return Content(content, "text/plain", Encoding.UTF8);
		}
	}
}
