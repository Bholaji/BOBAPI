using AutoMapper;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.DTO.UserDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BobAPI.Controllers
{
    [Route("api/user")]
	[ApiController]
	public class UserController(IUserService userService) : ControllerBase
	{
		private readonly IUserService _userService = userService;

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateUsers([FromBody] UserCompositeDTO userDTO)
		{
			var response = await _userService.CreateUser(userDTO);
			return Ok(response);
		
		}

		[HttpGet("getall")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 0)
		{
			PaginationDTO DTO = new()
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			};
			var response = await _userService.GetUsers(DTO);
			return Ok(response);
		}

		[HttpGet("get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAUser([FromQuery]Guid userId)
		{
			var response = await _userService.GetUser(userId);
			return Ok(response);
		}

		[HttpPost("updateuser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateUser([FromQuery] Guid userId, [FromBody] UpdateUserRequest DTO)
		{
			DTO.UserId = userId;
			var response = await _userService.UpdateUser(DTO);
			return Ok(response);
		}

		[HttpPost("updateaddress")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateAddress([FromQuery] Guid addressId, [FromBody] UserAddressDTO DTO)
		{
			DTO.AddressId = addressId;
			var response = await _userService.UpdateAddress(DTO);
			return Ok(response);
		}

		[HttpPost("updatepayroll")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdatePayroll([FromQuery] Guid payrollId, [FromBody] UserPayrollDTO DTO)
		{
			DTO.PayrollId = payrollId;
			var response = await _userService.UpdatePayroll(DTO);
			return Ok(response);
		}

		[HttpPost("updatesocial")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateSocial([FromQuery] Guid socialId, [FromBody] UserSocialDTO DTO)
		{
			DTO.SocialId = socialId;
			var response = await _userService.UpdateSocial(DTO);
			return Ok(response);
		}

		[HttpPost("updatefinancial")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateFinancial([FromQuery] Guid financialId, [FromBody] UserFinancialDTO DTO)
		{
			DTO.FinancialId = financialId;
			var response = await _userService.UpdateFinancial(DTO);
			return Ok(response);
		}

		[HttpPost("updatecontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateContact([FromQuery] Guid contactId, [FromBody] UserContactDTO DTO)
		{
			DTO.ContactId = contactId;
			var response = await _userService.UpdateContact(DTO);
			return Ok(response);
		}

		[HttpPost("updateemploymentinformation")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> UpdateEmploymentInformation([FromQuery] Guid employmentInformationId, [FromBody] UserEmploymentInformationDTO DTO)
		{
			DTO.EmploymentInformationId = employmentInformationId;
			var response = await _userService.UpdateEmploymentInformation(DTO);
			return Ok(response);
		}
	}
}
