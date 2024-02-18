using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Model.DTO.PostDTO
{
	public class DeletePostDTO
	{
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }

    }
}
