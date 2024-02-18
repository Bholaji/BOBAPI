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
        [JsonIgnore]
        public int PageNumber { get; set; } = 1;
		[JsonIgnore]
		public int PageSize { get; set; } = 0;
    }
}
