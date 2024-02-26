using Bob.Core.Services.IServices;
using Bob.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Bob.Model.DTO.TaskDTO;
using Bob.Model.DTO.PaginationDTO;

namespace BobAPI.Controllers
{
	[Route("api/task")]
	[ApiController]
	public class TaskController : Controller
	{
		private readonly ITaskService _taskService;
		public TaskController(ITaskService taskService)
        {
			_taskService = taskService;
		}

		[HttpPost("createtask")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateTask([FromBody] TaskRequestDTO DTO)
		{
			var response = await _taskService.CreateTask(DTO);
			return Ok(response);
		}

		[HttpPost("{userId}/updatetask/{taskId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateTask(Guid taskId,Guid userId, [FromBody] UpdateTaskDTO DTO)
		{
			DTO.TaskId = taskId;
			DTO.UserId = userId;
			var response = await _taskService.UpdateTask(DTO);
			return Ok(response);
		}

		[HttpPost("{taskId}/tooglestatus")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> ToogleStatus(Guid taskId, [FromBody] ToogleStatusDTO DTO)
		{
			DTO.TaskId = taskId;
			var response = await _taskService.ToogleStatus(DTO);
			return Ok(response);
		}

		[HttpGet("{userId}/getusertasks")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetUserTasks(Guid userId, [FromQuery]PaginationDTO DTO)
		{
			TaskPaginationDTO taskDTO = new()
			{
				PageSize = DTO.PageSize,
				PageNumber = DTO.PageNumber,
				UserId = userId
			};
			var response = await _taskService.GetUserTasks(taskDTO);
			return Ok(response);
		}

		[HttpGet("{userId}/get/{taskId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetATask(Guid userId,Guid taskId, [FromQuery] PaginationDTO DTO)
		{
			TaskPaginationDTO taskDTO = new()
			{
				PageSize = DTO.PageSize,
				PageNumber = DTO.PageNumber,
				UserId = userId,
				TaskId = taskId
			};
			var response = await _taskService.GetATask(taskDTO);
			return Ok(response);
		}
	}
}
