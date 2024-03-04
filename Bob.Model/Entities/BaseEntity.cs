using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public Guid OrganizationId { get; set; }
		[ForeignKey("OrganizationId")]
		[ValidateNever]
		public Organization organization { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set; }
	}
}
