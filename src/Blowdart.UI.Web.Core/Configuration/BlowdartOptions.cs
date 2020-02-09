// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Blowdart.UI.Web.Core.Configuration
{
    public class BlowdartOptions
    {
        public RunAt RunAt { get; set; }
        public WebAppOptions App { get; set; } = new WebAppOptions();

		public ISet<LogTarget> LogTargets { get; set; } = new HashSet<LogTarget>();

		public class WebAppOptions
        {
			public string Title { get; set; }
			public string FaviconsLocation { get; set; }

	        public RobotsTxtOptions RobotsTxt { get; set; } = new RobotsTxtOptions();

	        public class RobotsTxtOptions
	        {
		        public bool DisallowAll { get; set; }
	        }
        }
	}
}
