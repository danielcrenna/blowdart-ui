using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Examples.Models;
using Newtonsoft.Json;

namespace Demo.Web.Models
{
	public class WebSignInService : ISignInService
	{
		private readonly HttpClient _client;

		public WebSignInService(HttpClient client)
		{
			_client = client;
		}

		public async Task SignInAsync(SignInModel model)
		{
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
			await _client.PostAsync("server/signin", content);
		}

		public async Task SignOutAsync()
		{
			await _client.PutAsync("server/signout", null);
		}
	}
}
