
using Bob.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bob.Migrations.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<UserContact> UserContact { get; set; }
		public DbSet<UserAddress> UserAddresses { get; set; }
		public DbSet<UserSocial> UserSocials { get; set; }
		public DbSet<UserFinancial> UserFinancials { get; set; }
		public DbSet<UserPayroll> UserPayrolls { get; set; }
		public DbSet<UserEmploymentInformation> UserEmploymentInformations { get; set; }
		public DbSet<Organization> Organizations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(b =>
			{
				b.HasIndex(x => x.OrganizationId);
			});
		}

	}
}
