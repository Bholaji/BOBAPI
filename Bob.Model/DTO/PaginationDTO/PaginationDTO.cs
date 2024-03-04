using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.PaginationDTO
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 0;
    }
}
