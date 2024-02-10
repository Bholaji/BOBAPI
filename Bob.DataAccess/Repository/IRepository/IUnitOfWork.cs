﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IUserRepository User {get;}
		IAddressRepository Address { get;}
		IUserContactRepository Contact { get; }
	}
}
