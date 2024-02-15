﻿using Bob.DataAccess.Repository.IRepository;
using Bob.Migrations.Data;
using Bob.Model.Entities.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository
{
	public class CommentRepository : Repository<Comment>, ICommentRepository
	{
		ApplicationDbContext _db;

		public CommentRepository(ApplicationDbContext db): base(db)
        {
			_db = db;
        }
        public Comment UpdateAsync(Comment entity)
		{
			_db.Update(entity);
			return entity;
		}
	}
}
