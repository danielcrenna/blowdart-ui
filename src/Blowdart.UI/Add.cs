using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blowdart.UI
{
	public static class Add
	{
		public static void AddBlowdart(this IServiceCollection services, Action<PageMap> pageBuilder)
		{
			var pageMap = new PageMap();
			pageBuilder?.Invoke(pageMap);

			services.TryAddSingleton<ITypeResolver, ReflectionTypeResolver>();
			services.AddSingleton(pageMap);
		}
	}
}
