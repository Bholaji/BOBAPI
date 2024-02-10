﻿using Bob.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IUserContactRepository : IRepository<UserContact>
	{
		Task<UserContact> UpdateAsync (UserContact entity);
	}
}
