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
	public class AddressRepository : Repository<UserAddress>, IAddressRepository
	{
		private readonly ApplicationDbContext _db;
        public AddressRepository(ApplicationDbContext db):base(db)
        {
			_db = db;
        }
        public UserAddress UpdateAsync(UserAddress entity)
		{
			_db.UserAddresses.Update(entity);
			return entity;
		}     
	}
}
