﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO.PaginationDTO
{
	public class UserPaginationDTO : PaginationDTO
	{
        public Guid UserId { get; set; }
    }
}
