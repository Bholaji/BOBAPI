using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;

namespace Bob.Core.Services.IServices
{
    public interface IUserService
	{
		//Users
		Task<APIResponse<UserResponseDTO>> CreateUser(UserRequestDTO userDTO);
		Task<APIResponse<List<UserResponseDTO>>> GetUsers();
		Task<APIResponse<UserResponseDTO>> GetUser(Guid id);
		Task<APIResponse<UpdateUserDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDTO);
		//ADDRESS
		Task<APIResponse<UserAddressDTO>> CreateAddress(Guid id, UserAddressDTO userAddressDTO);
		Task<APIResponse<List<UserAddressDTO>>> GetAllAddress();
		Task<APIResponse<UserAddressDTO>> GetAddress(Guid id);
		Task<APIResponse<UserAddressDTO>> UpdateAddress(Guid id, UserAddressDTO userAddressDTO);
		//CONTACT
		Task<APIResponse<UserContactDTO>> CreateContact(Guid id, UserContactDTO userContactDTO);
		Task<APIResponse<List<UserContactDTO>>> GetAllContact();
		Task<APIResponse<UserContactDTO>> GetContact(Guid id);
		Task<APIResponse<UserContactDTO>> UpdateContact(Guid id, UserContactDTO userContactDTO);
	}
}
