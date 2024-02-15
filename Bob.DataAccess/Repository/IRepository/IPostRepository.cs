using Bob.Model.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IPostRepository: IRepository<Post>
	{
		Post UpdateAsync(Post entity);
	}
}
