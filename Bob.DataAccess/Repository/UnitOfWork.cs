using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;

namespace Bob.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepository User { get; private set; }
		public IAddressRepository Address { get; private set; }
		public IUserContactRepository Contact { get;private set; }

		private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
			Address = new AddressRepository(_db);
			Contact = new UserContactRepository(_db);

		}
	}
}
