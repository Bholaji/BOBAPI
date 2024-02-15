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
	public class UserPayrollRepository : Repository<UserPayroll>, IUserPayrollRepository
	{
		private readonly ApplicationDbContext _db;
        public UserPayrollRepository(ApplicationDbContext db):base(db)
        {
			_db=db;
        }
        public UserPayroll UpdateAsync(UserPayroll entity)
		{
			_db.UserPayrolls.Update(entity);
			return entity;
		}
	}
}
