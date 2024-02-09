using AutoMapper;
using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BobAPI.Controllers
{
    //[Route("api/bob/home")]
    [Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserService _userService;
		public HomeController(IUnitOfWork unitOfWork, IUserService userService)
        {
			_unitOfWork = unitOfWork;
			_userService = userService;
        }

        /*public IActionResult Index()
		{
			return View();
		}*/

		[HttpPost("createuser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateUsers([FromBody] UserRequestDTO userDTO)
		{
			var response = await _userService.CreateUser(userDTO);
			return Ok(response);
		
		}

		[HttpGet("getallusers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _userService.GetUsers();
			return Ok(response);
		}

		[HttpGet("{id:Guid}", Name = "getauser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAUser(Guid id)
		{
			var response = await _userService.GetUser(id);
			return Ok(response);
		}

		[HttpPut("{id:Guid}", Name = "updateuser")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateUsers(Guid id, [FromBody] UpdateUserDTO updateUserDTO)
		{
			var response = await _userService.UpdateUser(id, updateUserDTO);
			return Ok(response);
		}

	}
}
