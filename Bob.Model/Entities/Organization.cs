using System.ComponentModel.DataAnnotations;

namespace Bob.Model.Entities
{
	public class Organization
	{
		public Guid Id { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set; }
		[MaxLength(50)]
		public string Name { get; set; }
		[MaxLength(50)]
		public string? Domain { get; set; }
		[MaxLength(50)]
		public string? DomainSuffix { get; set; }
	}
}
