using System.ComponentModel.DataAnnotations;

namespace DataRequests
{
	public class BookRequest
	{
		[Required(ErrorMessage = "Please enter a valid media type")]
		
		public string MediaType { get; set; } 

		[Required(ErrorMessage = "please enter a valid Title")]
		public string Title { get; set; }

		[Required(ErrorMessage = "please enter a valid author")]
		public string Author { get; set; }
	}
}
