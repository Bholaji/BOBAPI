using Bob.Model.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface ICommentRepository: IRepository<Comment>
	{
		Comment UpdateAsync(Comment entity);
	}
}
