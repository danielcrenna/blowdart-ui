using System.Threading.Tasks;
using Blowdart.UI.Web.Core.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using TypeKitchen;

namespace Blowdart.UI.Web
{
	[HtmlTargetElement("appTitle")]
	public class TitleTagHelper : TagHelper
	{
		private const string DefaultTitle = @"Blowdart UI Demo";

		private readonly IHtmlHelper _htmlHelper;
		private readonly IOptionsSnapshot<BlowdartOptions> _options;

		public TitleTagHelper(IHtmlHelper htmlHelper, IOptionsSnapshot<BlowdartOptions> options)
		{
			_htmlHelper = htmlHelper;
			_options = options;
		}
		
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			((IViewContextAware) _htmlHelper).Contextualize(ViewContext);

			var tags = Pooling.StringBuilderPool.Scoped(sb =>
			{
				var title = _options.Value.App.Title ?? DefaultTitle;

				sb.AppendLine($@"<title>{title}</title>");
			});

			output.Content.SetHtmlContent(tags);
			output.TagName = null;

			return Task.CompletedTask;
		}
	}
}