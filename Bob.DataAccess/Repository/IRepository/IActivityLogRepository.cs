using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IActivityLogRepository: IRepository<ActivityLog>
	{
		ActivityLog Update(ActivityLog entity);
	}
}
