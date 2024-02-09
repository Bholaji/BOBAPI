using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepository User { get; private set; }

		private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);

		}

	}
}
