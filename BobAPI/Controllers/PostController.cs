using Bob.Core.Services.IServices;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.DTO.TaskDTO;
using Microsoft.AspNetCore.Mvc;

namespace BobAPI.Controllers
{
	[Route("api/post")]
	[ApiController]
	public class PostController(IPostService postService, ITaskService taskService) : ControllerBase
	{
		private readonly IPostService _postService = postService;
		private readonly ITaskService _taskService = taskService;


		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDTO postRequestDTO)
		{
			var response = await _postService.CreatePost(postRequestDTO);
			return Ok(response);

		}

		[HttpPost("{postId}/updatepost")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdatePost(Guid postId, [FromBody] UpdatePostRequestDTO postRequestDTO)
		{
			postRequestDTO.PostId = postId;
			var response = await _postService.UpdatePost(postRequestDTO);
			return Ok(response);
		}

		[HttpGet("getall")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetAllPost([FromQuery]PaginationDTO DTO)
		{
			var response = await _postService.GetPosts(DTO);
			return Ok(response);
		}

		[HttpGet("get/{postId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetPost(Guid postId)
		{
			var response = await _postService.GetAPost(postId);
			return Ok(response);
		}

		[HttpDelete("delete/{postId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeletePost(Guid postId)
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

		public async Task<IActionResult> GetComment(Guid postId, [FromQuery]PaginationDTO dto)
		{
			CommentPaginationDTO commentDto = new()
			{
				PostId = postId,
				PageNumber = dto.PageNumber,
				PageSize = dto.PageSize
			};
			var response = await _postService.GetComment(commentDto);
			return Ok(response);
		}

		[HttpDelete("{postId}/delete/{commentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeleteComment([FromRoute]Guid postId, [FromRoute] Guid commentId)
		{
			DeletePostDTO DTO = new()
			{
				PostId = postId,
				CommentId = commentId
			};
			var response = await _postService.DeleteAComment(DTO);
			return Ok(response);
		}
	}
}
