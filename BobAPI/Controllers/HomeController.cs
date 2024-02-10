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

		//USERS

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

		//ADDRESS

		[HttpPost("address/create/{id:Guid}", Name ="createadress")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> CreateAddress(Guid id, [FromBody] UserAddressDTO userAddressDTO)
		{
			var response = await _userService.CreateAddress(id, userAddressDTO);
			return Ok(response);
		}

		[HttpGet("address/getalladdress")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllAddresses()
		{
			var response = await _userService.GetAllAddress();
			return Ok(response);
		}

		[HttpGet("address/{id:Guid}", Name = "getaddress")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAddress(Guid id)
		{
			var response = await _userService.GetAddress(id);
			return Ok(response);
		}

		[HttpPut("address/{id:Guid}", Name = "updateaddress")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UserAddressDTO userAddressDTO)
		{
			var response = await _userService.UpdateAddress(id, userAddressDTO);
			return Ok(response);
		}

		//CONTACT

		[HttpPost("contact/create/{id:Guid}", Name = "createcontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> CreateContact(Guid id, [FromBody] UserContactDTO userContactDTO)
		{
			var response = await _userService.CreateContact(id, userContactDTO);
			return Ok(response);
		}

		[HttpGet("contact/getallcontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllContacts()
		{
			var response = await _userService.GetAllContact();
			return Ok(response);
		}

		[HttpGet("contact/{id:Guid}", Name = "getcontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetContact(Guid id)
		{
			var response = await _userService.GetContact(id);
			return Ok(response);
		}

		[HttpPut("contact/{id:Guid}", Name = "updatecontact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UserContactDTO userContactDTO)
		{
			var response = await _userService.UpdateContact(id, userContactDTO);
			return Ok(response);
		}
	}
}
