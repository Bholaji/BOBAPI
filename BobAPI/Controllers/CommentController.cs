using Bob.Core.Services;
using Bob.Core.Services.IServices;
using Bob.Model.DTO.CommentDTO;
using Bob.Model.DTO.PostDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Microsoft.AspNetCore.Mvc;

namespace BobAPI.Controllers
{
	[Route("api/comment")]
	[ApiController]
	public class CommentController(ICommentService commentService) : ControllerBase
	{
		private readonly ICommentService _commentService = commentService;

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreatePost([FromQuery] Guid userId, [FromBody] CreateCommentRequestDTO DTO)
		{
			DTO.UserId = userId;
			var response = await _commentService.CreateComment(DTO);
			return Ok(response);

		}

		[HttpPost("update")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateComment([FromQuery] Guid commentId, [FromBody] UpdateCommentDTO DTO)
		{
			DTO.CommentId = commentId;
			var response = await _commentService.UpdateComment(DTO);
			return Ok(response);
		}

		[HttpGet("getall")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetAllComment()
		{
			var response = await _commentService.GetComments();
			return Ok(response);
		}

		[HttpGet("get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetComment([FromQuery] Guid commentId)
		{
			var response = await _commentService.GetAComment(commentId);
			return Ok(response);
		}

		[HttpDelete("delete")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeleteComment([FromQuery] Guid commentId)
		{
			var response = await _commentService.DeleteAComment(commentId);
			return Ok(response);
		}

	}
}
