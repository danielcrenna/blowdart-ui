// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace Blowdart.UI.Web
{
	public class WebUserResolver : IUserResolver
	{
		private readonly IHttpContextAccessor _accessor;

		public WebUserResolver(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public IPrincipal GetCurrentUser()
		{
			return _accessor.HttpContext.User;
		}
	}
}