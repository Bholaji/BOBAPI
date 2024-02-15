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
	public class UserEmploymentInformationRepository : Repository<UserEmploymentInformation>, IUserEmploymentInformationRepository
	{
		private readonly ApplicationDbContext _db;
        public UserEmploymentInformationRepository(ApplicationDbContext db): base(db)
        {
			_db = db;
        }
        public UserEmploymentInformation UpdateAsync(UserEmploymentInformation entity)
		{
			_db.UserEmploymentInformations.Update(entity);
			return entity;
		}
	}
}
