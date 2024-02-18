using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bob.Model.DTO.PaginationDTO
{
	public class PostPaginationDTO: PaginationDTO
	{
		[JsonIgnore]
        public Guid PostId { get; set; }
    }
}
