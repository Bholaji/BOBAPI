using Bob.Core.Services;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
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
		public async Task<IActionResult> CreatePost([FromQuery] Guid userId, [FromBody] CreatePostRequestDTO postRequestDTO)
		{
			postRequestDTO.UserId = userId;
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

		public async Task<IActionResult> GetAllPost()
		{
			var response = await _postService.GetPosts();
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
	}
}
