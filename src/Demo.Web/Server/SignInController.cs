using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Examples.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Server
{
	public class AuthController : Controller
	{
		private static readonly string AuthScheme = IdentityConstants.ApplicationScheme;
		
		[HttpPost("server/signin")]
		public async Task<IActionResult> SignIn([FromBody] SignInModel model)
		{
			await HttpContext.SignOutAsync(AuthScheme);
			
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, model.Email)
			};
			var claimsIdentity = new ClaimsIdentity(claims, AuthScheme);
			var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };
			
			await HttpContext.SignInAsync(AuthScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
			return Ok();
		}

		[HttpPut("server/signout")]
		public async Task<IActionResult> SignOut()
		{
			await HttpContext.SignOutAsync(AuthScheme);
			return Ok();
		}
	}
}
