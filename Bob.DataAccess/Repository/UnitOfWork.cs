using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Microsoft.EntityFrameworkCore;

namespace Bob.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepository User { get; private set; }
		public IAddressRepository Address { get; private set; }
		public IUserContactRepository Contact { get;private set; }
		public IUserSocialRepository Social { get; private set; }
		public IUserPayrollRepository Payroll { get; private set; }
		public IUserFinancialRepository Financial { get; private set; }
		public IUserEmploymentInformationRepository EmploymentInformation { get; private set; }
		public IOrganizationRepository OrganizationRepository { get; private set; }
		public IPostRepository Post { get; private set; }
		public ICommentRepository Comment { get; private set; }
		public ITaskRepository UserTask { get; private set; }
		public IActivityLogRepository ActivityLog { get; private set; }
		public ITaskJobRepository TaskJob { get; private set; }

		private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
			Address = new AddressRepository(_db);
			Contact = new UserContactRepository(_db);
			Social = new UserSocialRepository(_db);
			Payroll = new UserPayrollRepository(_db);
			Financial = new UserFinancialRepository(_db);
			EmploymentInformation = new UserEmploymentInformationRepository(_db);
			OrganizationRepository = new OrganizationRepository(_db);
			Post = new PostRepository(_db);
			Comment = new CommentRepository(_db);
			UserTask = new TaskRepository(_db);
			ActivityLog = new ActivityLogRepository(_db);
			TaskJob = new TaskJobRepository(_db);
		}

		public void BeginTransaction()
		{
			_db.Database.BeginTransaction();
		}

		
		public void CommitTransaction()
		{
			_db.Database.CommitTransaction();
		}

		
		public void RollbackTransaction()
		{
			_db.Database.RollbackTransaction();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
