using System.ComponentModel.DataAnnotations;

namespace Bob.Model.Entities
{
	public class Organization
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreationDate { get; set; }
		public DateTime ModificationDate { get; set; }
		[MaxLength(50)]
		public string Name { get; set; }
		[MaxLength(50)]
		public string? Domain { get; set; }
		[MaxLength(50)]
		public string? DomainSuffix { get; set; }

        public IEnumerable<User> User { get; set; }
        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public IEnumerable<UserContact> UserContacts { get; set; }
        public IEnumerable<UserEmploymentInformation> UserEmploymentInformations { get; set; }
        public IEnumerable<UserFinancial> UserFinancials { get; set; }
        public IEnumerable<UserPayroll> UserPayrolls { get; set; }
		public IEnumerable<UserSocial> UserSocials { get; set; }
		public IEnumerable<UserTask> UserTasks { get; set; }
	}
}
