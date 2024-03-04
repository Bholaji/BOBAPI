using Bob.Model.Entities;
using System.Linq.Expressions;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageNumber = 1, int pageSize = 0);
		Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
		Task CreateAsync(T entity);
		Task RemoveAsync(T entity);
		Task SaveAsync();
		Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
	}
}