using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.Entities
{
	public class Role: BaseEntity
	{
		[MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public List<RolePermission> RolePermissions { get; set;}
		public List<User> User { get; set; }

	}
}
