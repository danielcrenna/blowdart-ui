namespace Blowdart.UI.Patterns
{
	public class Avatar
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string ImageUrl { get; set; }
	}
}
