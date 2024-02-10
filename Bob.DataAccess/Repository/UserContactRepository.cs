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
	public class UserContactRepository : Repository<UserContact>, IUserContactRepository
	{
		private readonly ApplicationDbContext _db;
        public UserContactRepository(ApplicationDbContext db):base(db)
        {
			_db = db;
        }
        public async Task<UserContact> UpdateAsync(UserContact entity)
		{
			_db.UserContact.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
