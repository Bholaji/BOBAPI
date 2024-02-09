using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO
{
	public class CreateUserDTO
	{
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
		public string? Surname { get; set; }
		public string? FullName { get; set; }
		public string? DispalyName { get; set; }
		public string? Country { get; set; }
		public string? State { get; set; }
		public string? PersonalEmail { get; set; }
		public string? PhoneNumber { get; set; }
	}
}
