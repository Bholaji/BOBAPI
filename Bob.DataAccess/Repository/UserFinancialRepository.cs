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
	public class UserFinancialRepository : Repository<UserFinancial>, IUserFinancialRepository
	{
		private readonly ApplicationDbContext _db;
        public UserFinancialRepository(ApplicationDbContext db):base(db)
        {
			_db = db;
        }
        public UserFinancial UpdateAsync(UserFinancial entity)
		{
			_db.UserFinancials.Update(entity);
			return entity;
		}
	}
}
