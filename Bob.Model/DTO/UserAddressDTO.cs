using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO
{
	public class UserAddressDTO
	{
		[MaxLength(50)]
		public string? AddressLine1 { get; set; }
		[MaxLength(50)]
		public string? AddressLine2 { get; set; }
		[MaxLength(50)]
		public string? City { get; set; }
		[MaxLength(50)]
		public string? PostalCode { get; set; }
		[MaxLength(50)]
		public string? Country { get; set; }
		[MaxLength(50)]
		public string? State { get; set; }
		[MaxLength(50)]
		public string? ModifiedBy { get; set; }
	}
}
