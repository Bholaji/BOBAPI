using Bob.Core.Services.IServices;
using Bob.Model.DTO.LeaveDTO;
using Microsoft.AspNetCore.Mvc;

namespace BobAPI.Controllers
{
	[ApiController]
	[Route("api/leave")]
	public class LeaveRequestController : Controller
	{
		private readonly ILeaveRequestService _leaveRequestService;
		public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
			_leaveRequestService = leaveRequestService;
        }

        [HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> RequestLeave([FromBody] LeaveRequestDTO DTO)
		{
			var response = await _leaveRequestService.RequestLeave(DTO);
			return Ok(response);

		}

		[HttpPost("edit/{leaveRequestId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> EditRequestLeave(Guid leaveRequestId, [FromBody] EditRequestLeaveDTO DTO)
		{
			DTO.LeaveRequestId = leaveRequestId;
			var response = await _leaveRequestService.EditRequestLeave(DTO);
			return Ok(response);

		}

		[HttpPost("{managerId}/tooglestatusapproval")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> ToogleStatusApproval(Guid managerId, [FromBody] LeaveApprovalDTO DTO)
		{
			DTO.ManagerId = managerId;
			var response = await _leaveRequestService.ToogleStatusApproval(DTO);
			return Ok(response);
		}

		[HttpGet("{userId}/getcarryover")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetCarryOverActivityForAUser(Guid userId)
		{
			var response = await _leaveRequestService.GetCarryOverActivityForAUser(userId);
			return Ok(response);
		}

		[HttpGet("{userId}/getleaverequest")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetLeaveRequestForAUser(Guid userId)
		{
			var response = await _leaveRequestService.GetLeaveRequestForAUser(userId);
			return Ok(response);
		}

		[HttpPost("{userId}/getleavebalance")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetLeaveBalnceBasedOnActivityType(Guid userId, [FromBody] GetCarryOverActivityRequestDTO DTO)
		{
			DTO.UserId = userId;
			var response = await _leaveRequestService.GetLeaveBalnceBasedOnActivityType(DTO);
			return Ok(response);
		}

		[HttpPost("{userId}/getleavedaysaccural")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetLeaveDaysAccuralBasedOnActivityType(Guid userId, [FromBody] GetCarryOverActivityRequestDTO DTO)
		{
			DTO.UserId = userId;
			var response = await _leaveRequestService.GetLeaveDaysAccuralBasedOnActivityType(DTO);
			return Ok(response);
		}


	}
}
