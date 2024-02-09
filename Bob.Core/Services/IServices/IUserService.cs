using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.UserDTO;

namespace Bob.Core.Services.IServices
{
    public interface IUserService
	{
		Task<APIResponse<UserResponseDTO>> CreateUser(UserRequestDTO userDTO);
		Task<APIResponse<List<UserResponseDTO>>> GetUsers();
		Task<APIResponse<UserResponseDTO>> GetUser(Guid id);
		Task<APIResponse<UpdateUserDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDTO);


	}
}
