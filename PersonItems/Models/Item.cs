using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PersonItems.Models
{
    public class Item
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
		
		[Required]
		[JsonProperty(PropertyName = "full name")]
		public string Fullname { get; set; }
		
		[Required]
		[JsonProperty(PropertyName = "bio")]
		public string Bio { get; set; }

		[JsonProperty(PropertyName = "Age")]
		public int Age { get; set; }
	}
}