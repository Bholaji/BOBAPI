using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.PostDTO;

namespace Bob.Core.Services.IServices
{
	public interface IPostService
	{

		Task<APIResponse<PostResponseDTO>> CreatePost(CreatePostRequestDTO postRequestDTO);
		Task<APIResponse<PostResponseDTO>> UpdatePost(UpdatePostRequestDTO postRequestDTO);
		Task<APIResponse<List<GetPostDTO>>> GetPosts();
		Task<APIResponse<GetPostDTO>> GetAPost(Guid id);
		Task<APIResponse<PostResponseDTO>> DeleteAPost(Guid id);
	}
}
