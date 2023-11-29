using System;
using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.Models
{
	//This attribute helps us to add description which can be seen in documents
	//We will move this from our POCO models to seperate PlatformType as per Code first approach
	//[GraphQLDescription("Represents any software or service that has CLI")]
	public class Platform
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }

		//[GraphQLDescription("Represents purchased license")] moved to PlatformType
		public string? LicenseKey { get; set; }

		public ICollection<Command>? Commands { get; set; } = new List<Command>();

	}
}

