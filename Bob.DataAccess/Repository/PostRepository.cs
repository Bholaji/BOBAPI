using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository
{
	public class PostRepository: Repository<Post>, IPostRepository
	{
		private readonly ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

		public Post UpdateAsync(Post entity)
		{
			_db.Update(entity);
			return entity;
		}
	}
}
