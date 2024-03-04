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
		public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestDTO DTO)
		{
			var response = await _taskService.CreateTask(DTO);
			return Ok(response);
		}

		[HttpPost("updatetask/{taskJobId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateTask(Guid taskJobId,[FromBody] UpdateTaskDTO DTO)
		{
			DTO.TaskJobId = taskJobId;
			var response = await _taskService.UpdateTask(DTO);
			return Ok(response);
		}

		[HttpPost("updatetaskrequestedfor/{taskJobId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateTaskWithRequestedFor(Guid taskJobId, [FromBody] UpdateTaskWithRequestedFor DTO)
		{
			DTO.TaskJobId = taskJobId;
			var response = await _taskService.UpdateTaskWithRequestedFor(DTO);
			return Ok(response);
		}

		[HttpPost("{taskId}/tooglestatus")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> ToogleStatus(Guid taskId,[FromBody] ToogleStatusDTO DTO)
		{
			DTO.TaskId = taskId;
			var response = await _taskService.ToogleStatus(DTO);
			return Ok(response);
		}

		[HttpGet("getusertasks")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetUserTasks(Guid userId, [FromQuery] PaginationDTO DTO)
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

		[HttpGet("get/{taskId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetATask(Guid taskId)
		{

			var response = await _taskService.GetATask(taskId);
			return Ok(response);
		}
	}
}
