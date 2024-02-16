using Bob.Core.Services;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Microsoft.AspNetCore.Mvc;

namespace BobAPI.Controllers
{
	[Route("api/post")]
	[ApiController]
	public class PostController(IPostService postService) : ControllerBase
	{
		private readonly IPostService _postService = postService;


		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDTO postRequestDTO)
		{
			var response = await _postService.CreatePost(postRequestDTO);
			return Ok(response);

		}

		[HttpPost("update", Name = "updatepost")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdatePost([FromQuery] Guid postId, [FromBody] UpdatePostRequestDTO postRequestDTO)
		{
			postRequestDTO.UserId = postId;
			var response = await _postService.UpdatePost(postRequestDTO);
			return Ok(response);
		}

		[HttpGet("getall")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetAllPost(int pageNumber = 1, int pageSize = 0)
		{
			var response = await _postService.GetPosts(pageSize: pageSize, pageNumber: pageNumber);
			return Ok(response);
		}

		[HttpGet("get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetPost([FromQuery] Guid postId)
		{
			var response = await _postService.GetAPost(postId);
			return Ok(response);
		}

		[HttpDelete("delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeletePost([FromQuery] Guid postId)
		{
			var response = await _postService.DeleteAPost(postId);
			return Ok(response);
		}


		[HttpPost("{postId}/createcomment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateComment(Guid postId, [FromBody] CreateCommentRequestDTO DTO)
		{
			DTO.PostId = postId;
			var response = await _postService.CreateComment(postId, DTO);
			return Ok(response);

		}

		[HttpPost("{postId}/updatecomment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateComment(Guid postId, [FromQuery] Guid commentId, [FromBody] UpdateCommentDTO DTO)
		{
			DTO.CommentId = commentId;
			DTO.PostId = postId;
			var response = await _postService.UpdateComment(DTO);
			return Ok(response);
		}

		[HttpGet("{postId}/getcomment")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetComment(Guid postId, int pageNumber = 1, int pageSize = 0)
		{
			var response = await _postService.GetComment(postId, pageSize: pageSize, pageNumber: pageNumber);
			return Ok(response);
		}

		[HttpDelete("{postId}/delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeleteComment(Guid postId, [FromQuery] Guid commentId)
		{
			var response = await _postService.DeleteAComment(postId, commentId);
			return Ok(response);
		}
	}
}
