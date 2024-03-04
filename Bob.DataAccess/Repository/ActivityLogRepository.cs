using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities;
using Bob.Model.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = Bob.Model.Entities.UserTask;

namespace Bob.DataAccess.Repository
{
	public class ActivityLogRepository : Repository<ActivityLog>, IActivityLogRepository
	{
		private readonly ApplicationDbContext _db;
        public ActivityLogRepository(ApplicationDbContext db):base(db)
        {
			_db = db;
        }
        public ActivityLog Update(ActivityLog entity)
		{
			_db.Update(entity);
			return entity;
		}
	}
}
