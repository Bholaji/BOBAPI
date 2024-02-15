using Bob.Model.DTO.CommentDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Services.IServices
{
	public interface ICommentService
	{
		Task<APIResponse<CommentResponseDTO>> CreateComment(CreateCommentRequestDTO DTO);
		Task<APIResponse<CommentResponseDTO>> UpdateComment(UpdateCommentDTO DTO);
		Task<APIResponse<List<GetCommentDTO>>> GetComments();
		Task<APIResponse<GetCommentDTO>> GetAComment(Guid id);
		Task<APIResponse<CommentResponseDTO>> DeleteAComment(Guid id);
	}
}
