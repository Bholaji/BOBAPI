using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO
{
	public class UserAddressDTO
	{
		[JsonIgnore]
        public Guid AddressId { get; set; }
        [MaxLength(50)]
		public string? AddressLine1 { get; set; }
		[MaxLength(50)]
		public string? AddressLine2 { get; set; }
		[MaxLength(50)]
		public string? City { get; set; }
		public int? PostalCode { get; set; }
		[MaxLength(50)]
		public string? Country { get; set; }
		[MaxLength(50)]
		public string? State { get; set; }
		[MaxLength(50)]
		public string? ModifiedBy { get; set; }
		public Guid? OrganizationId { get; set; }
	}
}
