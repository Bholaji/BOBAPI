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
	public class UserSocialRepository : Repository<UserSocial>, IUserSocialRepository
	{
		private readonly ApplicationDbContext _db;
        public UserSocialRepository(ApplicationDbContext db):base(db)
        {
			_db = db;
        }
        public UserSocial UpdateAsync(UserSocial entity)
		{
			_db.UserSocials.Update(entity);
			return entity;
		}
	}
}
