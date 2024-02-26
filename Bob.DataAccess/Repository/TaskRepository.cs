using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities;

namespace Bob.DataAccess.Repository
{
	public class TaskRepository : Repository<UserTask>, ITaskRepository
	{
		private readonly ApplicationDbContext _db;
        public TaskRepository(ApplicationDbContext db): base(db)
        {
			_db = db;
        }
        public UserTask Update(UserTask entity)
		{
			_db.Update(entity);

			return entity;
		}

	}
}
