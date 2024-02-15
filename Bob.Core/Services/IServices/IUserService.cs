using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.DTO.UserDTO;

namespace Bob.Core.Services.IServices
{
	public interface IUserService
	{
		//Users
		//Task<APIResponse<UserResponseDTO>> CreateUser(UserRequestDTO userDTO);
		Task<APIResponse<List<UserResponseDTO>>> GetUsers();
		Task<APIResponse<UserResponseDTO>> GetUser(Guid id);
		Task<APIResponse<UserCompositeDTO>> CreateUser(UserCompositeDTO userCompositeDTO);
		Task<APIResponse<UpdateUserRequest>> UpdateUser(Guid id, UpdateUserRequest userCompositeDTO);
		Task<APIResponse<UserAddressDTO>> UpdateAddress(Guid id, UserAddressDTO userCompositeDTO);
		Task<APIResponse<UserPayrollDTO>> UpdatePayroll(Guid id, UserPayrollDTO userCompositeDTO);
		Task<APIResponse<UserSocialDTO>> UpdateSocial(Guid id, UserSocialDTO userCompositeDTO);
		Task<APIResponse<UserFinancialDTO>> UpdateFinancial(Guid id, UserFinancialDTO userCompositeDTO);
		Task<APIResponse<UserContactDTO>> UpdateContact(Guid id, UserContactDTO userCompositeDTO);
		Task<APIResponse<UserEmploymentInformationDTO>> UpdateEmploymentInformation(Guid id, UserEmploymentInformationDTO userCompositeDTO);
	}
}