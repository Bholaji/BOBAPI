using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PaginationDTO;

namespace Bob.Core.Services.IServices
{
	public interface IPostService
	{

		Task<APIResponse<PostResponseDTO>> CreatePost(CreatePostRequestDTO postRequestDTO);
		Task<APIResponse<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDTO);
		Task<APIResponse<List<GetPostDTO>>> GetPosts(PaginationDTO DTO);
		Task<APIResponse<GetPostDTO>> GetAPost(Guid postId);
		Task<APIResponse<PostResponseDTO>> DeleteAPost(Guid postId);

		Task<APIResponse<CommentResponseDTO>> CreateComment(Guid postId, CreateCommentRequestDTO DTO);
		Task<APIResponse<CommentResponseDTO>> UpdateComment(UpdateCommentDTO DTO);
		Task<APIResponse<List<GetCommentDTO>>> GetComment(CommentPaginationDTO DTO);
		Task<APIResponse<CommentResponseDTO>> DeleteAComment(DeletePostDTO DTO);
	}
}
