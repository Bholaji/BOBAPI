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
	public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
	{
		private readonly ApplicationDbContext _db;
        public OrganizationRepository(ApplicationDbContext db):base(db)
        {
			_db=db;
        }
        public Organization UpdateAsync(Organization entity)
		{
			_db.Organizations.Update(entity);
			return entity;
		}
	}
}
