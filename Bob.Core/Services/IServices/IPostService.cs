using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.CommentDTO;

namespace Bob.Core.Services.IServices
{
	public interface IPostService
	{

		Task<APIResponse<PostResponseDTO>> CreatePost(CreatePostRequestDTO postRequestDTO);
		Task<APIResponse<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDTO);
		Task<APIResponse<List<GetPostDTO>>> GetPosts(int pageNumber = 1, int pageSize = 0);
		Task<APIResponse<GetPostDTO>> GetAPost(Guid id);
		Task<APIResponse<PostResponseDTO>> DeleteAPost(Guid id);

		Task<APIResponse<CommentResponseDTO>> CreateComment(Guid postId, CreateCommentRequestDTO DTO);
		Task<APIResponse<CommentResponseDTO>> UpdateComment(UpdateCommentDTO DTO);
		Task<APIResponse<List<GetCommentDTO>>> GetComment(Guid postId, int pageNumber = 1, int pageSize = 0);
		Task<APIResponse<CommentResponseDTO>> DeleteAComment(Guid postId,Guid id);
	}
}
