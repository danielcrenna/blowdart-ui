using System.Threading.Tasks;

namespace Demo.Examples.Models
{
	public interface ISignInService
	{
		Task SignInAsync(SignInModel model);
		Task SignOutAsync();
	}
}
