
using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities;

namespace Bob.DataAccess.Repository
{
	public class UserRepository: Repository<User>, IUserRepository
	{
		private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public User UpdateAsync(User entity)
        {
			_db.Users.Update(entity);
			return entity;
		}

	}
}
