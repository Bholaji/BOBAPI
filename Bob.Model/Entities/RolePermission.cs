using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class RolePermission
	{
		public Guid Id { get; set; }
		public Guid RoleId { get; set; }
		[ForeignKey(nameof(RoleId))]
		[ValidateNever]
		public Role Role { get; set; }
		public Guid PermissionId { get; set; }
		[ForeignKey(nameof(PermissionId))]
		[ValidateNever]
		public Permission Permission { get; set; }
	}
}
