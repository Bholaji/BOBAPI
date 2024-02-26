using Bob.Model.Entities;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface ITaskRepository: IRepository<UserTask>
	{
		UserTask Update(UserTask entity);
	}
}
