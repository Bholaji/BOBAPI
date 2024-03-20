
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using Bob.Model.Enums;
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
		public DbSet<RolePermission> RolePermissions { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<UserTask> Tasks { get; set; }
		public DbSet<ActivityLog> ActivityLogs { get; set; }
		public DbSet<TaskJob> TaskJobs { get; set; }
		public DbSet<Manager> Managers { get; set; }
		public DbSet<UserTimeOff> UserTimeOffs { get; set; }
		public DbSet<LeaveBalanceActivity> LeaveBalanceActivities { get; set; }
		public DbSet<LeaveDaysAccural> LeaveDaysAccurals { get; set; }
		public DbSet<LeaveRequest> LeaveRequests { get; set; }
		public DbSet<CarryOverActivity> CarryOverActivities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<User>()
					.HasOne(u => u.organization)
					.WithMany(x => x.User)
					.HasForeignKey(u => u.OrganizationId)
					.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserTask>()
					.HasOne(u => u.TaskJobs)
					.WithMany(x => x.UserTasks)
					.HasForeignKey(u => u.TaskJobId)
					.OnDelete(DeleteBehavior.Restrict);

			//Composite index of employee id and organizationid
			modelBuilder.Entity<UserEmploymentInformation>()
				.HasIndex(u => new { u.OrganizationId, u.EmployeeID })
				.IsUnique();

			//Composite index of USER id and accural date
			modelBuilder.Entity<LeaveDaysAccural>()
				.HasIndex(u => new { u.UserId, u.AccuralDate, u.ActivityType })
				.IsUnique();


			modelBuilder.Entity<User>()
				.HasIndex(x => x.Email).IsUnique();

			modelBuilder.Entity<UserEmploymentInformation>()
				.HasIndex(x => x.EmployeeID);

			modelBuilder.Entity<Organization>()
				.HasIndex(x => x.Domain).IsUnique();

			modelBuilder.Entity<Permission>()
				.HasIndex(x => x.Name).IsUnique();

			modelBuilder.Entity<Role>()
				.HasIndex(x => x.Name).IsUnique();

			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.PersonalEmail).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.PhoneNumber).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.MobileNumber).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.PassportNumber).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.NationalId).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.SSN).IsUnique();
			modelBuilder.Entity<UserContact>()
				.HasIndex(x => x.TaxIdNumber).IsUnique();

			modelBuilder.Entity<UserFinancial>()
				.HasIndex(x => x.AccountNumber).IsUnique();

			modelBuilder.Entity<User>()
				.HasOne(u => u.Role)
				.WithMany(x => x.User)
				.HasForeignKey(u => u.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<RolePermission>()
				.HasOne(rp => rp.Role)
				.WithMany(r => r.RolePermissions)
				.HasForeignKey(rp => rp.RoleId)
				.OnDelete(DeleteBehavior.Restrict);
		}

	}
}
