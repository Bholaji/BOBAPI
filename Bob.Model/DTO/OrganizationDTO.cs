using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO
{
	public class OrganizationDTO
	{
		public DateTime? CreationDate { get; set; }
		public DateTime? ModificationDate { get; set; }
		[MaxLength(50)]
		public string? Name { get; set; }
		[MaxLength(50)]
		public string? Domain { get; set; }
		[MaxLength(50)]
		public string? DomainSuffix { get; set; }
	}
}
